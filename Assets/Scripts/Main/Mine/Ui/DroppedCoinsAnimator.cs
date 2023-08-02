namespace Main.Mine.Ui
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DG.Tweening;
    using Main.Currency.Coins;
    using Main.Currency.Coins.Ui;
    using UnityEngine;
    using UnityEngine.Pool;
    using Object = UnityEngine.Object;
    using Random = UnityEngine.Random;

    [Serializable]
    public class DroppedCoinsAnimator
    {
        [SerializeField]
        private CoinsHandler handler;

        [SerializeField]
        private CoinsHandlerUi coinsHandlerUi;
        
        [SerializeField]
        private DroppedCoin coinPrefab;

        [SerializeField]
        private Transform container;
        
        [SerializeField]
        private Transform dropPoint;
        
        [SerializeField]
        private Vector2 dropOffset;

        [SerializeField]
        private int maxAmountOfCoins = 5;

        [SerializeField]
        private float fallSpeed = 2000f;
        
        [SerializeField]
        private float moveSpeed = 2000f;

        private ObjectPool<DroppedCoin> _pool;

        private List<DroppedCoin> _coins = new List<DroppedCoin>();
        
        public void Initialize()
        {
            _pool = new ObjectPool<DroppedCoin>
            (
                CreateFunc,
                ActionOnGet,
                ActionOnRelease,
                ActionOnDestroy,
                false,
                maxAmountOfCoins,
                maxAmountOfCoins
            );
        }

        public void Dispose()
        {
            for (var i = 0; i < _coins.Count; )
            {
                _pool.Release(_coins[i]);
            }
            
            _pool.Dispose();
            _pool = null;
        }
        
        private void ActionOnDestroy(DroppedCoin coin)
        {
            Object.Destroy(coin.gameObject);
        }

        private void ActionOnRelease(DroppedCoin coin)
        {
            _coins.Remove(coin);
            coin.gameObject.SetActive(false);
        }

        private void ActionOnGet(DroppedCoin coin)
        {
            _coins.Add(coin);
            coin.gameObject.SetActive(true);
        }

        private DroppedCoin CreateFunc()
        {
            return Object.Instantiate(coinPrefab, container);
        }

        public async void Drop(int value)
        {
            SpawnCoins(value);

            coinsHandlerUi.BlockTextUpdate();

            var movements = new Task[_coins.Count];

            for (var i = 0; i < _coins.Count; i++)
            {
                movements[i] = MoveCoin(_coins[i]);
            }

            await Task.WhenAll(movements);
            
            coinsHandlerUi.UnblockTextUpdate();
        }

        private void SpawnCoins(int value)
        {
            var coinsToSpawn = Mathf.Min(value, maxAmountOfCoins);

            for (var i = 0; i < coinsToSpawn; i++)
            {
                var coin = _pool.Get();

                var x = Random.Range(-dropOffset.x, dropOffset.x);
                var y = Random.Range(-dropOffset.y, dropOffset.y);
                var offset = new Vector2(x, y);

                coin.transform.position = dropPoint.position + (Vector3)offset;

                SetCoinValue(value, i, coinsToSpawn, coin);
            }
        }

        private static void SetCoinValue(int value, int i, int coinsToSpawn, DroppedCoin coin)
        {
            if (i < coinsToSpawn - 1)
            {
                coin.Value = value / coinsToSpawn;
                
                return;
            }

            coin.Value = value / coinsToSpawn + value % coinsToSpawn;
        }

        private async Task MoveCoin(DroppedCoin coin)
        {
            var coinPosition = coin.transform.position;

            var groundPosition = new Vector3
            (
                coinPosition.x,
                dropPoint.transform.position.y - dropOffset.y
            );
            
            var distanceToGround = Mathf.Abs((coinPosition - groundPosition).magnitude);
            var endPointPosition = coinsHandlerUi.CoinIcon.transform.position;
            var distanceToEndPoint = Mathf.Abs((coinPosition - endPointPosition).magnitude);
            
            var sequence = DOTween.Sequence();

            sequence
                .Append(coin.transform.DOMove(groundPosition, distanceToGround / fallSpeed))
                .Append(coin.transform.DOMove(endPointPosition, distanceToEndPoint / moveSpeed));
            
            await sequence.Play().AsyncWaitForCompletion();

            handler.Add(coin.Value);
            
            coinsHandlerUi.Add(coin.Value);
            
            _pool.Release(coin);
        }
    }
}
