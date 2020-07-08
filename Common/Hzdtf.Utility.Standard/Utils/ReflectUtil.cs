using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;

namespace Hzdtf.Utility.Standard.Utils
{
    /// <summary>
    /// 反射辅助类
    /// @ 黄振东
    /// </summary>
    public static class ReflectUtil
    {
        /// <summary>
        /// 任务全名称前辍
        /// </summary>
        private const string PFX_TASK_FULL_NAME = "System.Threading.Tasks.Task";

        /// <summary>
        /// 创建类型实例
        /// </summary>
        /// <typeparam name="T">实例类型</typeparam>
        /// <param name="path">路径</param>
        /// <returns>类型实例</returns>
        public static T CreateInstance<T>(string path)
            where T : class => CreateInstance(path) as T;

        /// <summary>
        /// 创建类型的实例
        /// </summary>
        /// <typeparam name="T">实例类型</typeparam>
        /// <param name="type">类型</param>
        /// <returns>实例</returns>
        public static T CreateInstance<T>(this Type type)
            where T : class
        {
            return type.Assembly.CreateInstance(type.FullName) as T;
        }

        /// <summary>
        /// 创建对象实例
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>对象实例</returns>
        public static object CreateInstance(string path)
        {
            string assemblyName;
            var className = GetClassAndAssemblyFullName(path, out assemblyName);

            object[] parames;
            int startIndex = className.LastIndexOf("("), endIndex = className.LastIndexOf(")");

            if (startIndex == -1 || endIndex == -1)
            {
                parames = null;
            }
            else
            {
                // 如果有参数
                string[] pNameValues = className.Substring(startIndex + 1, className.Length - startIndex - 2).Split(',');
                parames = new object[pNameValues.Length];
                for (int i = 0; i < pNameValues.Length; i++)
                {
                    string[] mapNameValue = pNameValues[i].Split(' ');
                    if (string.IsNullOrEmpty(mapNameValue[1]))
                    {
                        continue;
                    }

                    switch (mapNameValue[0])
                    {
                        case "int":
                        case "Int32":
                            parames[i] = Convert.ToInt32(mapNameValue[1]);

                            break;

                        case "short":
                        case "Int16":
                            parames[i] = Convert.ToInt16(mapNameValue[1]);

                            break;

                        case "Int64":
                            parames[i] = Convert.ToInt64(mapNameValue[1]);

                            break;

                        case "decimal":
                        case "Decimal":
                            parames[i] = Convert.ToDecimal(mapNameValue[1]);

                            break;

                        case "float":
                        case "Float":
                            parames[i] = Convert.ToSingle(mapNameValue[1]);

                            break;

                        case "double":
                        case "Double":
                            parames[i] = Convert.ToDouble(mapNameValue[1]);

                            break;

                        case "DateTime":
                            parames[i] = Convert.ToDateTime(mapNameValue[1]);

                            break;

                        default:
                            parames[i] = mapNameValue[1];

                            break;
                    }
                }

                className = className.Substring(0, startIndex);
            }
            
            Assembly assembly = GetAssembly(assemblyName);
            if (parames == null)
            {
                return assembly.CreateInstance(className);
            }
            else
            {
                return assembly.CreateInstance(className, false, BindingFlags.Default, null, parames, null, null);
            }
        }

        /// <summary>
        /// 设置属性值
        /// </summary>
        /// <param name="property">属性</param>
        /// <param name="instance">实例</param>
        /// <param name="value">值</param>
        public static void SetPropertyValue(this PropertyInfo property, object instance, object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return;
            }

            if (property.PropertyType.IsEnum)
            {
                string val = value.ToString();
                if (string.IsNullOrWhiteSpace(val))
                {
                    return;
                }
                property.SetValue(instance, Enum.Parse(property.PropertyType, val));
            }
            else if (property.PropertyType == typeof(int) || property.PropertyType == typeof(int?))
            {
                if (string.IsNullOrWhiteSpace(value.ToString()))
                {
                    return;
                }
                property.SetValue(instance, Convert.ToInt32(value));
            }
            else if (property.PropertyType == typeof(int) || property.PropertyType == typeof(int?))
            {
                if (string.IsNullOrWhiteSpace(value.ToString()))
                {
                    return;
                }
                property.SetValue(instance, Convert.ToUInt32(value));
            }
            else if (property.PropertyType == typeof(short) || property.PropertyType == typeof(short?))
            {
                if (string.IsNullOrWhiteSpace(value.ToString()))
                {
                    return;
                }
                property.SetValue(instance, Convert.ToInt16(value));
            }
            else if (property.PropertyType == typeof(ushort) || property.PropertyType == typeof(ushort?))
            {
                if (string.IsNullOrWhiteSpace(value.ToString()))
                {
                    return;
                }
                property.SetValue(instance, Convert.ToUInt16(value));
            }
            else if (property.PropertyType == typeof(UInt64) || property.PropertyType == typeof(UInt64?))
            {
                if (string.IsNullOrWhiteSpace(value.ToString()))
                {
                    return;
                }
                property.SetValue(instance, Convert.ToUInt64(value));
            }
            else if (property.PropertyType == typeof(long) || property.PropertyType == typeof(long?))
            {
                if (string.IsNullOrWhiteSpace(value.ToString()))
                {
                    return;
                }
                property.SetValue(instance, Convert.ToInt64(value));
            }
            else if (property.PropertyType == typeof(byte) || property.PropertyType == typeof(byte?))
            {
                if (string.IsNullOrWhiteSpace(value.ToString()))
                {
                    return;
                }
                property.SetValue(instance, Convert.ToByte(value));
            }
            else if (property.PropertyType == typeof(sbyte) || property.PropertyType == typeof(sbyte?))
            {
                if (string.IsNullOrWhiteSpace(value.ToString()))
                {
                    return;
                }
                property.SetValue(instance, Convert.ToSByte(value));
            }
            else if (property.PropertyType == typeof(float) || property.PropertyType == typeof(float?))
            {
                if (string.IsNullOrWhiteSpace(value.ToString()))
                {
                    return;
                }
                property.SetValue(instance, Convert.ToSingle(value));
            }
            else if (property.PropertyType == typeof(double) || property.PropertyType == typeof(double?))
            {
                if (string.IsNullOrWhiteSpace(value.ToString()))
                {
                    return;
                }
                property.SetValue(instance, Convert.ToDouble(value));
            }
            else if (property.PropertyType == typeof(decimal) || property.PropertyType == typeof(decimal?))
            {
                if (string.IsNullOrWhiteSpace(value.ToString()))
                {
                    return;
                }
                property.SetValue(instance, Convert.ToDecimal(value));
            }
            else if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?))
            {
                if (string.IsNullOrWhiteSpace(value.ToString()))
                {
                    return;
                }
                property.SetValue(instance, Convert.ToDateTime(value));
            }
            else if (property.PropertyType == typeof(bool) || property.PropertyType == typeof(bool?))
            {
                if (string.IsNullOrWhiteSpace(value.ToString()))
                {
                    return;
                }
                property.SetValue(instance, Convert.ToBoolean(value));
            }
            else if (property.PropertyType == typeof(string))
            {
                property.SetValue(instance, value);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(value.ToString()))
                {
                    return;
                }

                property.SetValue(instance, value);
            }
        }

        /// <summary>
        /// 获取方法的特性
        /// </summary>
        /// <typeparam name="AttributeT">特性类型</typeparam>
        /// <param name="method">方法</param>
        /// <returns>特性</returns>
        public static AttributeT GetAttribute<AttributeT>(this MethodInfo method)
            where AttributeT : Attribute
        {
            object[] atts = method.GetCustomAttributes(typeof(AttributeT), false);
            return atts.IsNullOrLength0() ? null : atts[0] as AttributeT;
        }

        /// <summary>
        /// 获取类型的特性
        /// </summary>
        /// <typeparam name="AttributeT">特性类型</typeparam>
        /// <param name="type">类型</param>
        /// <returns>特性</returns>
        public static AttributeT GetAttribute<AttributeT>(this Type type)
            where AttributeT : Attribute
        {
            object[] atts = type.GetCustomAttributes(typeof(AttributeT), false);
            return atts.IsNullOrLength0() ? null : atts[0] as AttributeT;
        }

        /// <summary>
        /// 为枚举获取特性
        /// </summary>
        /// <typeparam name="AttributeT">特性类型</typeparam>
        /// <param name="enumType">枚举类型</param>
        /// <param name="enumField">枚举字段</param>
        /// <returns>特性</returns>
        public static AttributeT GetAttributeForEnum<AttributeT>(this Type enumType, string enumField)
            where AttributeT : Attribute
        {
            var atts = enumType.GetField(enumField).GetCustomAttributes(typeof(AttributeT), false);
            return atts.IsNullOrLength0() ? null : atts[0] as AttributeT;
        }

        /// <summary>
        /// 获取程序集，如果为空，则返回当前程序集
        /// </summary>
        /// <param name="assemblyOrFile">程序集名称或文件</param>
        /// <returns>程序集</returns>
        public static Assembly GetAssembly(string assemblyOrFile)
        {
            if (string.IsNullOrWhiteSpace(assemblyOrFile))
            {
                return Assembly.GetExecutingAssembly();
            }
            else if (assemblyOrFile.EndsWith(".dll", StringComparison.OrdinalIgnoreCase))
            {
                return assemblyOrFile.Contains("\\") || assemblyOrFile.Contains("/")
                    ? Assembly.LoadFile(assemblyOrFile)
                    : Assembly.Load(assemblyOrFile);
            }
            else
            {
                return Assembly.Load(assemblyOrFile);
            }
        }

        /// <summary>
        /// 获取类全名以及程序集名
        /// </summary>
        /// <param name="path">路径，程序集与类名以,分隔；如果没有程序集，则不用传,号</param>
        /// <param name="assemblyName">程序集名</param>
        /// <returns>类全名</returns>
        public static string GetClassAndAssemblyFullName(string path, out string assemblyName)
        {
            string[] temp = path.Split(',');
            string className = null;
            if (temp.Length == 2)
            {
                assemblyName = temp[0].Trim();
                className = temp[1].Trim();
            }
            else
            {
                assemblyName = null;
                className = temp[0].Trim();
            }

            return className;
        }

        /// <summary>
        /// 执行静态方法
        /// </summary>
        /// <param name="methodFullPath">方法全路径，全路径以.描述，最后一个.肯定是方法名</param>
        /// <param name="parames">参数数组</param>
        /// <returns>返回值</returns>
        public static object InvokeStaticMethod(string methodFullPath, params object[] parames)
        {
            MethodInfo method;

            return InvokeStaticMethod(methodFullPath, out method);
        }

        /// <summary>
        /// 执行静态方法
        /// </summary>
        /// <param name="methodFullPath">方法全路径，全路径以.描述，最后一个.肯定是方法名</param>
        /// <param name="method">方法</param>
        /// <param name="parames">参数数组</param>
        /// <returns>返回值</returns>
        public static object InvokeStaticMethod(string methodFullPath, out MethodInfo method, params object[] parames)
        {
            method = GetMethod(methodFullPath);

            return method.Invoke(null, parames);
        }

        /// <summary>
        /// 获取方法
        /// </summary>
        /// <param name="methodFullPath">方法全路径，全路径以.描述，最后一个.肯定是方法名</param>
        /// <returns>方法</returns>
        public static MethodInfo GetMethod(string methodFullPath)
        {
            if (string.IsNullOrWhiteSpace(methodFullPath))
            {
                return null;
            }

            string assemblyName;
            var className = GetClassAndAssemblyFullName(methodFullPath, out assemblyName);
            var assembly = GetAssembly(assemblyName);

            var lastPointIndex = className.LastIndexOf(".");
            return assembly.GetType(className.Substring(0, lastPointIndex)).GetMethod(className.Substring(lastPointIndex + 1));
        }

        /// <summary>
        /// 获取方法名
        /// </summary>
        /// <param name="methodFullPath">方法全路径，全路径以.描述，最后一个.肯定是方法名</param>
        /// <param name="classFullPath">类全路径</param>
        /// <returns>方法名</returns>
        public static string GetMethodName(string methodFullPath, out string classFullPath)
        {
            string assemblyName;
            var methodName = GetMethodName(methodFullPath, out assemblyName, out classFullPath);
            if (!string.IsNullOrWhiteSpace(assemblyName))
            {
                classFullPath = assemblyName + "," + classFullPath;
            }

            return methodName;
        }

        /// <summary>
        /// 获取方法名
        /// </summary>
        /// <param name="methodFullPath">方法全路径，全路径以.描述，最后一个.肯定是方法名</param>
        /// <param name="assemblyName">程序集名</param>
        /// <param name="classFullPath">类全路径</param>
        /// <returns>方法名</returns>
        public static string GetMethodName(string methodFullPath, out string assemblyName, out string classFullPath)
        {
            assemblyName = classFullPath = null;
            if (string.IsNullOrWhiteSpace(methodFullPath))
            {
                return null;
            }

            var className = GetClassAndAssemblyFullName(methodFullPath, out assemblyName);
            var lastPointIndex = className.LastIndexOf(".");
            classFullPath = className.Substring(0, lastPointIndex);

            return className.Substring(lastPointIndex + 1);
        }

        /// <summary>
        /// 创建实例来自方法全路径
        /// </summary>
        /// <param name="methodFullPath">方法全路径，全路径以.描述，最后一个.肯定是方法名</param>
        /// <param name="method">方法</param>
        /// <returns>实例</returns>
        public static object CreateInstanceFromMethodFullPath(string methodFullPath, out MethodInfo method)
        {
            string assemblyName;
            string classFullPath;
            string methodName = GetMethodName(methodFullPath, out assemblyName, out classFullPath);

            if (!string.IsNullOrWhiteSpace(assemblyName))
            {
                classFullPath = assemblyName + "," + classFullPath;
            }
            var instance = CreateInstance(classFullPath);
            method = instance.GetType().GetMethod(methodName);

            return instance;
        }

        /// <summary>
        /// 创建实例来自方法全路径
        /// </summary>
        /// <param name="methodFullPath">方法全路径，全路径以.描述，最后一个.肯定是方法名</param>
        /// <param name="method">方法</param>
        /// <returns>实例</returns>
        public static T CreateInstanceFromMethodFullPath<T>(string methodFullPath, out MethodInfo method)
            where T : class => CreateInstanceFromMethodFullPath(methodFullPath, out method) as T;

        /// <summary>
        /// 创建实例来自方法全路径
        /// </summary>
        /// <param name="methodFullPath">方法全路径，全路径以.描述，最后一个.肯定是方法名</param>
        /// <returns>实例</returns>
        public static object CreateInstanceFromMethodFullPath(string methodFullPath)
        {
            MethodInfo method;

            return CreateInstanceFromMethodFullPath(methodFullPath, out method);
        }

        /// <summary>
        /// 创建实例来自方法全路径
        /// </summary>
        /// <param name="methodFullPath">方法全路径，全路径以.描述，最后一个.肯定是方法名</param>
        /// <returns>实例</returns>
        public static T CreateInstanceFromMethodFullPath<T>(string methodFullPath)
            where T : class => CreateInstanceFromMethodFullPath(methodFullPath) as T;

        /// <summary>
        /// 判断方法返回值是否为void
        /// </summary>
        /// <param name="method">方法</param>
        /// <returns>方法返回值是否为void</returns>
        public static bool IsMethodReturnVoid(this MethodInfo method)
        {
            return method != null ? IsTypeVoid(method.ReturnType) : false;
        }

        /// <summary>
        /// 判断方法类型返回值是否为void
        /// </summary>
        /// <param name="type">方法类型</param>
        /// <returns>方法返回值是否为void</returns>
        public static bool IsTypeVoid(this Type type)
        {
            return type != null ? "System.Void".Equals(type.FullName) : false;
        }

        /// <summary>
        /// 判断方法返回值是否为任务（异步方法）
        /// </summary>
        /// <param name="method">方法</param>
        /// <returns>方法返回值是否为任务（异步方法）</returns>
        public static bool IsMethodReturnTask(this MethodInfo method)
        {
            return method != null ? IsTypeTask(method.ReturnType) : false;
        }

        /// <summary>
        /// 判断类型是否为任务
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>类型是否为任务</returns>
        public static bool IsTypeTask(this Type type)
        {
            return type != null ? type.FullName.StartsWith(PFX_TASK_FULL_NAME) : false;
        }

        /// <summary>
        /// 判断类型是否不带泛型的任务
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>类型是否不带泛型的任务</returns>
        public static bool IsTypeNotGenericityTask(this Type type)
        {
            return type != null ? type.FullName.Equals(PFX_TASK_FULL_NAME) : false;
        }

        /// <summary>
        /// 判断类型是否带泛型的任务
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>判断类型是否带泛型的任务</returns>
        public static bool IsTypeGenericityTask(this Type type)
        {
            if (IsTypeTask(type))
            {
                return type.FullName.Length > PFX_TASK_FULL_NAME.Length && type.FullName.Contains("[[") && type.FullName.Contains("]]");
            }

            return false;
        }

        /// <summary>
        /// 获取类类型
        /// </summary>
        /// <param name="classFullPath">类全路径</param>
        /// <returns>类类型</returns>
        public static Type GetClassType(string classFullPath)
        {
            string assemblyName;
            var className = GetClassAndAssemblyFullName(classFullPath, out assemblyName);
            var assembly = GetAssembly(assemblyName);

            return assembly.GetType(className);
        }

        /// <summary>
        /// 从当前执行的程序集里获取指定接口类型的所有实现类类型数组
        /// </summary>
        /// <param name="interfaceType">接口类型</param>
        /// <returns>实现类类型数组</returns>
        public static Type[] GetImplClassTypeFromCurrExecAssembly(Type interfaceType)
            => GetImplClassType(new Assembly[] { Assembly.GetExecutingAssembly() }, interfaceType);

        /// <summary>
        /// 获取指定接口类型的所有实现类类型数组
        /// </summary>
        /// <param name="interfaceType">接口类型</param>
        /// <returns>实现类类型数组</returns>
        public static Type[] GetImplClassType(Type interfaceType)
            => GetImplClassType(AppDomain.CurrentDomain.GetAssemblies(), interfaceType);

        /// <summary>
        /// 在程序集数组里获取指定接口类型的所有实现类类型数组
        /// </summary>
        /// <param name="assemblies">程序集数组</param>
        /// <param name="interfaceType">接口类型</param>
        /// <returns>实现类类型数组</returns>
        public static Type[] GetImplClassType(Assembly[] assemblies, Type interfaceType)
        {
            if (assemblies.IsNullOrLength0())
            {
                return null;
            }

            return assemblies.SelectMany(a => a.GetTypes().Where(t => t.GetInterfaces().Contains(interfaceType))).ToArray();
        }

        /// <summary>
        /// 获取方法的返回值类型，如果是Task，则获取Task.Result的类型
        /// </summary>
        /// <param name="method">方法</param>
        /// <returns>方法的返回值类型</returns>
        public static Type GetReturnValueType(this MethodInfo method)
        {
            if (method == null)
            {
                return null;
            }

            Type targetType = null;

            // 判断返回类型是否Task
            if (method.ReturnType.IsTypeNotGenericityTask())
            {
                return null;
            }
            else if (method.ReturnType.IsTypeGenericityTask())
            {
                targetType = method.ReturnType.GetProperty("Result").PropertyType;
            }
            else
            {
                targetType = method.ReturnType;
            }

            return targetType;
        }
    }
}
