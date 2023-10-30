namespace OoManager.Models
{
    public enum Grade
    {
        PreSchool_5 = 5, PreSchool_6, PreSchool_7,
        PrimarySchool_1, PrimarySchool_2, PrimarySchool_3, PrimarySchool_4, PrimarySchool_5, PrimarySchool_6,
        MiddleSchool_1, MiddleSchool_2, MiddleSchool_3, 
        HighSchool_1, HighSchool_2, HighSchool_3,
    }
    public enum MemberState
    {
        Normal, Rest, Attention, Cancel
    }

    public enum OoMessageType
    {
        MemberAdd, MemberUpdate, MemberDelete
    }


}
