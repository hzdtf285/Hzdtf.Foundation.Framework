using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Hzdtf.Utility.Standard.Utils
{
    /// <summary>
    /// Bash命令辅助类
    /// @ 黄振东
    /// </summary>
    public static class BashCommandUtil
    {
        /// <summary>
        /// 执行Bash命令
        /// </summary>
        /// <param name="command">命令</param>
        /// <returns>输出返回值</returns>
        public static string ExecBashCommand(this string command)
        {
            string err;

            return ExecBashCommand(command, out err);
        }

        /// <summary>
        /// 执行Bash命令
        /// </summary>
        /// <param name="commands">命令数组</param>
        /// <returns>输出返回值</returns>
        public static string[] ExecBashCommand(this string[] commands)
        {
            string err;

            return ExecBashCommand(commands, out err);
        }

        /// <summary>
        /// 执行Bash命令
        /// </summary>
        /// <param name="commands">命令数组</param>
        /// <param name="err">错误</param>
        /// <returns>输出返回值</returns>
        public static string[] ExecBashCommand(this string[] commands, out string err)
        {
            err = null;
            if (commands.IsNullOrLength0())
            {
                return null;
            }

            var re = new string[commands.Length];
            for (var i = 0; i < re.Length; i++)
            {
                re[i] = commands[i].ExecBashCommand(out err);
                if (string.IsNullOrWhiteSpace(err))
                {
                    continue;
                }

                return re;
            }

            return re;
        }

        /// <summary>
        /// 执行Bash命令
        /// </summary>
        /// <param name="command">命令</param>
        /// <param name="err">错误</param>
        /// <returns>输出返回值</returns>
        public static string ExecBashCommand(this string command, out string err)
        {
            err = null;
            if (string.IsNullOrWhiteSpace(command))
            {
                return null;
            }
            string output = null;
            using (var proc = new Process())
            {
                proc.StartInfo.FileName = "/bin/bash";
                proc.StartInfo.Arguments = $"-c \" {command}\"";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.Start();

                output = proc.StandardOutput.ReadToEnd();
                err = proc.StandardError.ReadToEnd();

                proc.WaitForExit();
            }

            return output;
        }
    }
}
