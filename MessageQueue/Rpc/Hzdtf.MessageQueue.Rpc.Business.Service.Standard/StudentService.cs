using Hzdtf.Autofac.Extend.Standard;
using Hzdtf.MessageQueue.Rpc.Business.Contract.Standard;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Model.Return;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hzdtf.MessageQueue.Rpc.Business.Service.Standard
{
    /// <summary>
    /// 学生服务
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class StudentService : IStudentService
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="student">学生</param>
        /// <returns>是否成功</returns>
        public bool Add(StudentInfo student)
        {
            return true;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="student">学生</param>
        /// <returns>是否成功</returns>
        public bool Adds(IList<StudentInfo> students)
        {
            return true;
        }

        public StudentInfo Get(int id)
        {
            return new StudentInfo()
            {
                Id = 1,
                Name = "杨玉英"
            };
        }

        public IList<StudentInfo> Query()
        {
            return new List<StudentInfo>()
            {
                new StudentInfo()
                {
                    Id = 1,
                    Name = "要不1"
                }, new StudentInfo()
                {
                    Id = 2,
                    Name = "要不2"
                }
            };
        }

        public ReturnInfo<StudentInfo> Get2(int id)
        {
            var re = new ReturnInfo<StudentInfo>();
            re.Data = Get(id);

            return re;
        }

        public ReturnInfo<IList<StudentInfo>> Query2()
        {
            var re = new ReturnInfo<IList<StudentInfo>>();
            re.Data = Query();

            return re;
        }

        public BasicReturnInfo Test()
        {
            var re = new BasicReturnInfo();
            re.SetCodeMsg(1, "fds", "des");

            return re;
        }

        public Task TestTask1Async()
        {
            return Task.Run(() =>
            {
                Thread.Sleep(5000);
            });
        }

        public Task<int> TestTask2Async()
        {
            return Task<int>.Run(() =>
            {
                Thread.Sleep(5000);

                return 10;
            });
        }

        public Task<StudentInfo> TestTask3Async()
        {
            return Task<StudentInfo>.Run(() =>
            {
                Thread.Sleep(5000);

                return Get(1333);
            });
        }

        public Task<IList<StudentInfo>> TestTask4Async()
        {
            return Task<IList<StudentInfo>>.Run(() =>
            {
                Thread.Sleep(5000);

                return Query();
            });
        }

        public Task<object> TestTask5Async()
        {
            return Task<object>.Run(() =>
            {
                Thread.Sleep(5000);
                object o = new List<StudentInfo>()
                {
                    new StudentInfo()
                    {
                        Id = 1,
                        Name = "dsf"
                    },
                    new StudentInfo()
                    {
                        Id = 23,
                        Name = "dfsafasfsa"
                    }
                };
                return o;
            });
        }
    }
}
