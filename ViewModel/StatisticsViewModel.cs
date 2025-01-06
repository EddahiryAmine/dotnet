namespace EmsiStudySpace.ViewModel
{
    public class StatisticsViewModel
    {
        public int TotalProfessors { get; set; }
        public int TotalStudents { get; set; }
        public int TotalModules { get; set; }
        public int TotalGroups { get; set; }
        public int TotalFiliere { get; set; }
        public int TotalRooms { get; set; }

        public List<GroupsPerFiliere> GroupsPerFiliere { get; set; }
        public List<StudentsPerFiliere> StudentsPerFiliere { get; set; }
    }

    public class GroupsPerFiliere
    {
        public string FiliereName { get; set; }
        public int GroupCount { get; set; }
    }

    public class StudentsPerFiliere
    {
        public string FiliereName { get; set; }
        public int StudentCount { get; set; }
    }
}
