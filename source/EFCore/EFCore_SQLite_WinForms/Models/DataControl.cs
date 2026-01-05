using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Xml.Linq;

namespace EFCore_SQLite_WinForms.Models
{
    public interface IDataControl
    {
        public void DataAdd(ILogger _logger, ModelContext _context, ComboBox cmbSchool, TextBox tbName);
    }


    public class DataControl : IDataControl
    {
        public void DataAdd(ILogger _logger, ModelContext _context, ComboBox cmbSchool, TextBox tbName)
        {
            try
            {
                int addSchoolId = 1;
                var query = from sc in _context?.schools
                            select new { ID = sc.id, NAME = sc.name };
                var schoolList = query.ToList();

                foreach (var s in schoolList)
                {
                    if (s.NAME == cmbSchool.Text) addSchoolId = s.ID;
                }

                var addData = new Student
                {
                    name = tbName.Text,
                    schoolId = addSchoolId
                };

                _context?.students.Add(addData);
                _context?.SaveChanges();
                _logger?.Log(LogLevel.Information, $"학생 추가 : {tbName.Text}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                _logger?.Log(LogLevel.Error, ex.Message);
                throw;
            }
        }
    }
}
