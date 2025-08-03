using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Alumno
{
    [Key]
    public int AlumnoID { get; set; }

    [Required]
    [MaxLength(20)]
    public string Rut { get; set; }

    [Required]
    [MaxLength(50)]
    public string Nombre { get; set; }

    [Required]
    [MaxLength(50)]
    public string Apellido { get; set; }

    [Required]
    [MaxLength(100)]
    public string Correo { get; set; }

    [Required]
    [MaxLength(20)]
    public string Telefono { get; set; }
}

public class Curso
{
    [Key]
    public int CursoID { get; set; }

    [Required]
    [MaxLength(100)]
    public string NombreCurso { get; set; }

    [Required]
    public DateTime FechaInicio { get; set; }

    [Required]
    [MaxLength(50)]
    public string Horario { get; set; }
}

public class Inscripcion
{
    [Key]
    public int InscripcionID { get; set; }

    public int AlumnoID { get; set; }

    public int CursoID { get; set; }

    [Required]
    public DateTime FechaInscripcion { get; set; }

    [ForeignKey("AlumnoID")]
    public Alumno Alumno { get; set; }

    [ForeignKey("CursoID")]
    public Curso Curso { get; set; }
}
