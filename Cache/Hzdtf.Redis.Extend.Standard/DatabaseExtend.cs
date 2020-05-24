using StackExchange.Redis;
using System;
using Hzdtf.Utility.Standard.Utils;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

namespace Hzdtf.Redis.Extend.Standard
{
    /// <summary>
    /// 数据库扩展类
    /// @ 黄振东
    /// </summary>
    public static class DatabaseExtend
    {
        #region 对象

        /// <summary>
        /// 对象设置
        /// </summary>
        /// <param name="db">数据库</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expiry">时间间隔</param>
        public static void ObjectSet(this IDatabase db, RedisKey key, object value, TimeSpan? expiry = null)
        {
            HashEntry[] hashEntries = RedisUtil.ToHashEntrys(value);
            if (hashEntries.IsNullOrLength0())
            {
                return;
            }

            if (db.KeyExists(key))
            {
                db.KeyDelete(key);
            }

            db.HashSet(key, hashEntries);
            if (expiry != null)
            {
                db.KeyExpire(key, expiry);
            }
        }

        /// <summary>
        /// 异步对象设置
        /// </summary>
        /// <param name="db">数据库</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expiry">时间间隔</param>
        /// <returns>任务</returns>
        public static Task ObjectSetAsync(this IDatabase db, RedisKey key, object value, TimeSpan? expiry = null)
        {
            return Task.Factory.StartNew(() =>
            {
                ObjectSet(db, key, value, expiry);
            });
        }

        /// <summary>
        /// 对象获取
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="db">数据库</param>
        /// <param name="key">键</param>
        /// <returns>对象</returns>
        public static T ObjectGet<T>(this IDatabase db, RedisKey key)
            where T : class => RedisUtil.FromHashEntrys<T>(db.HashGetAll(key));

        /// <summary>
        /// 异步对象获取
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="db">数据库</param>
        /// <param name="key">键</param>
        /// <returns>任务</returns>
        public static Task<T> ObjectGetAsync<T>(this IDatabase db, RedisKey key)
            where T : class
        {
            return Task.Factory.StartNew<T>(() =>
            {
                return ObjectGet<T>(db, key);
            });
        }

        #endregion

        #region Json对象

        /// <summary>
        /// Json对象设置
        /// </summary>
        /// <param name="db">数据库</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expiry">时间间隔</param>
        public static void JsonObjectSet(this IDatabase db, RedisKey key, object value, TimeSpan? expiry = null)
        {
            if (value == null)
            {
                return;
            }

            var jsonStr = JsonUtil.SerializeIgnoreNull(value);
            db.StringSet(key, jsonStr, expiry);
        }

        /// <summary>
        /// Json异步对象设置
        /// </summary>
        /// <param name="db">数据库</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expiry">时间间隔</param>
        /// <returns>任务</returns>
        public static Task JsonObjectSetAsync(this IDatabase db, RedisKey key, object value, TimeSpan? expiry = null)
        {
            return Task.Factory.StartNew(() =>
            {
                JsonObjectSet(db, key, value, expiry);
            });
        }

        /// <summary>
        /// Json对象获取
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="db">数据库</param>
        /// <param name="key">键</param>
        /// <returns>对象</returns>
        public static T JsonObjectGet<T>(this IDatabase db, RedisKey key)
            where T : class => JsonUtil.Deserialize<T>(db.StringGet(key));

        /// <summary>
        /// Json异步对象获取
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="db">数据库</param>
        /// <param name="key">键</param>
        /// <returns>任务</returns>
        public static Task<T> JsonObjectGetAsync<T>(this IDatabase db, RedisKey key)
            where T : class
        {
            return Task.Factory.StartNew<T>(() =>
            {
                return JsonObjectGet<T>(db, key);
            });
        }

        #endregion

        #region 锁

        /// <summary>
        /// 锁住可用的资源，默认5秒超时，因是主动轮询机制，性能较差
        /// </summary>
        /// <param name="db">数据库</param>
        /// <param name="key">键</param>
        /// <param name="action">动作</param>
        /// <param name="retryIntervalMillisecond">重试间隔毫秒数</param>
        [Obsolete("此方法是主动轮询检查资源是否已释放，增加了Redis负担，应弃用；应改为发布订阅模式，请使用IConnectionMultiplexer.LockTake方法")]
        public static void LockTake(this IDatabase db, RedisKey key, Action action, int retryIntervalMillisecond = 200)
        {
            LockTake(db, key, StringUtil.NewShortGuid(), action, TimeSpan.FromSeconds(5), retryIntervalMillisecond);
        }

        /// <summary>
        /// 锁住可用的资源，因是主动轮询机制，性能较差
        /// </summary>
        /// <param name="db">数据库</param>
        /// <param name="key">键</param>
        /// <param name="expiry">时间间隔</param>
        /// <param name="action">动作</param>
        /// <param name="retryIntervalMillisecond">重试间隔毫秒数</param>
        [Obsolete("此方法是主动轮询检查资源是否已释放，增加了Redis负担，应弃用；应改为发布订阅模式，请使用IConnectionMultiplexer.LockTake方法")]
        public static void LockTake(this IDatabase db, RedisKey key, Action action, TimeSpan expiry, int retryIntervalMillisecond = 200)
        {
            LockTake(db, key, StringUtil.NewShortGuid(), action, expiry, retryIntervalMillisecond);
        }

        /// <summary>
        /// 锁住可用的资源，默认5秒超时，因是主动轮询机制，性能较差
        /// </summary>
        /// <param name="db">数据库</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="action">动作</param>
        /// <param name="retryIntervalMillisecond">重试间隔毫秒数</param>
        [Obsolete("此方法是主动轮询检查资源是否已释放，增加了Redis负担，应弃用；应改为发布订阅模式，请使用IConnectionMultiplexer.LockTake方法")]
        public static void LockTake(this IDatabase db, RedisKey key, RedisValue value, Action action, int retryIntervalMillisecond = 200)
        {
            LockTake(db, key, value, action, TimeSpan.FromSeconds(5), retryIntervalMillisecond);
        }

        /// <summary>
        /// 锁住可用的资源，因是主动轮询机制，性能较差
        /// </summary>
        /// <param name="db">数据库</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expiry">时间间隔</param>
        /// <param name="action">动作</param>
        /// <param name="retryIntervalMillisecond">重试间隔毫秒数</param>
        [Obsolete("此方法是主动轮询检查资源是否已释放，增加了Redis负担，应弃用；应改为发布订阅模式，请使用IConnectionMultiplexer.LockTake方法")]
        public static void LockTake(this IDatabase db, RedisKey key, RedisValue value, Action action, TimeSpan expiry, int retryIntervalMillisecond = 200)
        {
            while (true)
            {
                bool canLock = db.LockTake(key, value, expiry);
                if (canLock)
                {
                    try
                    {
                        action();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message, ex);
                    }
                    finally
                    {
                        db.LockReleaseAsync(key, value);
                    }

                    return;
                }
                else
                {
                    Thread.Sleep(retryIntervalMillisecond);
                }
            }
        }

        #endregion
    }
}