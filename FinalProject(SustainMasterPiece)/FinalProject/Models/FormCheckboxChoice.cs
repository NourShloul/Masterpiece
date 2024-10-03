using System;
using System.Collections.Generic;

namespace FinalProject.Models;

public partial class FormCheckboxChoice
{
    public int Id { get; set; }

    public int SubmissionId { get; set; }

    public string CheckboxOption { get; set; } = null!;

    public virtual Order Submission { get; set; } = null!;
}
