namespace Backend_InternalQM.Entities
{
    public class ExaminationCycle
    {
        public long Id { get; set; }

        public string Type { get; set; } = null!;

        public string CycleName { get; set; } = null!;

        public DateOnly DateMonth { get; set; }

        public long? CreatedBy { get; set; }

        public DateOnly? CreatedAt { get; set; }

        public long? UpdatedBy { get; set; }

        public DateOnly? UpdatedAt { get; set; }

        public long? DeleteBy { get; set; }

        public DateOnly? DeleteAt { get; set; }
    }
}
