using System.ComponentModel.DataAnnotations;

namespace MMASimulation.Shared.Models.Utils.Faces
{
    public class Face
    {
        [Key]
        public int DbId { get; set; }
        public int Id { get; set; }
        public double Fatness { get; set; }
        public string Color { get; set; }
        public Head Head { get; set; }
        public List<Eyebrow> Eyebrows { get; set; }
        public List<Eye> Eyes { get; set; }
        public Nose Nose { get; set; }
        public Mouth Mouth { get; set; }
        public Hair Hair { get; set; }
    }

    public class Head
    {

        [Key]
        public int DbId { get; set; }
        public int Id { get; set; }
    }

    public class Eyebrow
    {

        [Key]
        public int DbId { get; set; }
        public int Id { get; set; }
        public string LR { get; set; }
        public int Cx { get; set; }
        public int Cy { get; set; }
    }

    public class Eye
    {

        [Key]
        public int DbId { get; set; }
        public int Id { get; set; }
        public string LR { get; set; }
        public int Cx { get; set; }
        public int Cy { get; set; }
        public double Angle { get; set; }
    }

    public class Nose
    {

        [Key]
        public int DbId { get; set; }
        public int Id { get; set; }
        public string LR { get; set; }
        public int Cx { get; set; }
        public int Cy { get; set; }
        public double Size { get; set; }
        public bool Flip { get; set; }

    }

    public class Mouth
    {

        [Key]
        public int DbId { get; set; }
        public int Id { get; set; }
        public int Cx { get; set; }
        public int Cy { get; set; }

    }

    public class Hair
    {

        [Key]
        public int DbId { get; set; }
        public int Id { get; set; }
    }
}
