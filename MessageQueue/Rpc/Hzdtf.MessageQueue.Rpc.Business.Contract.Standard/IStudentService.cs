using Hzdtf.Utility.Standard.Model.Return;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.MessageQueue.Rpc.Business.Contract.Standard
{
    /// <summary>
    /// 学生服务接口
    /// @ 黄振东
    /// </summary>
    public interface IStudentService
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="student">学生</param>
        /// <returns>是否成功</returns>
        bool Add(StudentInfo student);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="student">学生</param>
        /// <returns>是否成功</returns>
        bool Adds(IList<StudentInfo> students);

        StudentInfo Get(int id);

        IList<StudentInfo> Query();

        ReturnInfo<StudentInfo> Get2(int id);

        ReturnInfo<IList<StudentInfo>> Query2();

        BasicReturnInfo Test();

        Task TestTask1Async();

        Task<int> TestTask2Async();

        Task<StudentInfo> TestTask3Async();

        Task<IList<StudentInfo>> TestTask4Async();

        Task<object> TestTask5Async();
    }
}
