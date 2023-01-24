﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamBilet2.Models
{
    public class Team
    {
        public int Id { get; set; }
        public int PositionId { get; set; }
        [StringLength(maximumLength:50)]
        public string? ImageUrl { get; set; }
        [StringLength(maximumLength: 50)]

        public string FullName { get; set; }

        [StringLength(maximumLength: 300)]

        public string Descriptiom { get; set; }
        [StringLength(maximumLength: 150)]

        public string? TwitterUrl { get; set; }
        [StringLength(maximumLength: 150)]

        public string? FbUrl { get; set; }
        [StringLength(maximumLength: 150)]

        public string? InstaUrl { get; set; }
        [StringLength(maximumLength: 150)]

        public string? LnUrl { get; set; }


        public Position? Position { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

    }
}
