using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRestAPI.DAL.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        /// <summary>
        /// Student's first name does not necessarily need to be capped. However, we need to consider the toll VARCHAR(max) has.
        /// </summary>
        [MaxLength(250)]
        public string FirstName { get; set; }
        [MaxLength(250)]
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
    }
}
