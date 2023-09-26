using System;
using OoManager.Common;

namespace OoManager.Models
{
    public class Member
    {
        public string member_class { get; set; } = "월화수목금";
        public int member_grade { get; set; } = 14;
        public string member_grade_str { get; set; } = "중1";
        public string member_money { get; set; } = "150000";
        public string member_motherphone { get; set; } = "010-0000-0000";
        public string member_name { get; set; } = string.Empty;
        public string member_status { get; set; } = "재원";
        public string member_text { get; set; } = "임시텍스트";
        public int member_xp { get; set; } = 10;
        public string member_xp_log { get; set; } = "회원등록 10xp";
        public int mid { get; set; } = 0;
        public string wid { get; set; } = string.Empty;
    }
}
