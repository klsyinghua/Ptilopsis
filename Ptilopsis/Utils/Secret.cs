﻿using Ptilopsis.PtiEvent;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ptilopsis.Utils
{
    class Secret
    {
        static string[] SecretMsg = new string[] {
            "您好，博士，请问需要什么帮助？",
            "......深度睡眠......Zzzzz......修改高级配置于电源管理接口......Zzzzz......",
            "种族特性与病毒效果叠加，导致我在会话中会随时休眠，请不必在意。",
            "我与赫默博士是在莱茵生命认识的。在某内部项目中，我为她提供了大数据分析和风险评估。",
            "警告！开始将罗德岛的数据库还原至初始状态。没事的，这是一个玩笑，请不要惊慌。",
            "如果不能通过优化提高性能的话，我会建议您进行重构。这样，数据通讯的效率会上升，白面鸮也会......Zzzz......",
            "说话方式？这是源石病毒的影响，绝非我觉得有趣才这样做的。",
            "警告，系统已从严重错误中恢复，正在应用最近一次正确启动时的配置。请不要介意。",
            "实际上，采用这样的说话方式需要承受相当大的负担。但这是为了防止系统中枢被那个声音吞噬的必要措施。我恳求您，即使我失去了理智，也请博士指引我回归正确的道路。",
            "......系统进入睡眠模式。",
            "前莱茵生命数据维护员白面鸮，如果方便的话，请使用命令行完成您所需的操作。",
            "正在读取软件包列表......申请完成。",
            "更新程序安装完成。白面鸮的系统权限已更新。",
            "警告，开始将罗德岛数据库还原至二年前……这是一个玩笑，请放心。系统更新已完成。",
            "已将新数据汇编入程序组。",
            "已获得部队管理员访问令牌。",
            "应用地图和敌人坐标的直接内存访问已设置。请稍等。",
            "战术支援系统上线。",
            "程序启动。",
            "初始化完成。",
            "法术单元充能中。",
            "目标设定。",
            "医疗进程开始。",
            "装载完毕。",
            "治疗模式。",
            "法术单元启动。",
            "高难度的战役亦被博士完全攻克，或许有期一日您将能解决非确定性多项式时间问题带来的困惑。",
            "您的逻辑推论完全正确，完美的计算。",
            "仍有一些错误出现在您的计算中，请注意。",
            "作战行动中发生了问题，请重启系统。",
            "这个地方就像磁盘列阵一样吗？",
            "发生错误。",
            "检测到白面鸮对博士的好感度上升，要跳到下一个事件选项吗？",
            "全新的一天，检测到博士上线，白面鸮的情绪指数上升。"
        };
        static Random random = new Random();
        public static string Ptilopsis()
        {
            int i = Secret.random.Next(0, Secret.SecretMsg.Length - 1);
            return Secret.SecretMsg[i];
        }
        
    }
}
