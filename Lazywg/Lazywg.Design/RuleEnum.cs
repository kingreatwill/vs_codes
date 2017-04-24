using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazywg.Design
{
    /// <summary>
    /// 规则缓存
    /// </summary>
    public enum ClearRuleEnum
    {
        /// <summary>
        /// 时间
        /// </summary>
        UseTime,

        /// <summary>
        /// 次数
        /// </summary>
        UseCount,

        /// <summary>
        /// 内存
        /// </summary>
        Memory,

        /// <summary>
        /// 缓存条数
        /// </summary>
        Count,

        /// <summary>
        /// 默认清除方式
        /// </summary>
        Default
    }

    public class ClearRule {

        public ClearRuleEnum RuleType { get; set; }

        public ClearRule() {

        }
       public int LowUseCount { get; set; }
    }

    public class UseTimeRule:ClearRule {

        public UseTimeRule() {
            RuleType = ClearRuleEnum.UseTime;
        }

        /// <summary>
        /// 秒 每多少秒清除一次
        /// </summary>
        public long Second { get; set; }
    }

    public class UseCountRule : ClearRule
    {

        public UseCountRule()
        {
            RuleType = ClearRuleEnum.UseCount;
        }

        /// <summary>
        /// 使用次数 低于该次数全部清除
        /// </summary>
        public int MinUseCount { get; set; }
    }

    public class MemoryRule : ClearRule
    {
        public MemoryRule()
        {
            RuleType = ClearRuleEnum.Memory;
        }

        /// <summary>
        /// 内存使用大于当前最大限制值时 根据其他规则清除
        /// </summary>
        public int MaxMemory { get; set; }
    }

    public class CountRule : ClearRule
    {
        public CountRule()
        {
            RuleType = ClearRuleEnum.Count;
        }

        /// <summary>
        /// 最大存储条数
        /// </summary>
        public int MaxCount { get; set; }
    }
}
