﻿namespace DataBaseTools.Models
{
    public class SftpModel
    {
        public string Name { get; set; } = string.Empty;
        public bool IsDirectory { get; set; } = false;
        public double? FileSize { get; set; }
    }
}
