using System.ComponentModel;
using EFCore_SQLite_WinForms.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EFCore_SQLite_WinForms;

public partial class Form1 : Form
{
    private readonly ILogger? _logger;
    private readonly ModelContext? _context;
    private readonly IDataControl _DataControl;

    private int updateId { get; set; }

    public Form1(ILogger<Form1> logger, ModelContext context, IDataControl DataControl)
    {
        InitializeComponent();
        _logger = logger;
        _context = context!;
        _DataControl = DataControl;

        dataGridView1.ReadOnly = true;
        dataGridView1.AllowUserToAddRows = false;
        dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    }

    protected override void OnLoad(EventArgs e)
    {
        try
        {
            base.OnLoad(e);

            _context?.Database.EnsureCreated();
            _context?.students.Load();
            dataGridView1.DataSource = _context?.students.Local.ToBindingList();

            cmbSchool.DataSource = _context?.schools.Select(p => p.name).ToList();

            _logger?.Log(LogLevel.Information, $"프로그램이 시작되었습니다.");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            _logger?.Log(LogLevel.Error, ex.Message);
            throw;
        }
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        try
        {
            base.OnClosing(e);
            _context?.Dispose();
            _logger?.Log(LogLevel.Information, $"프로그램이 종료되었습니다.");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            _logger?.Log(LogLevel.Error, ex.Message);
            throw;
        }
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        _DataControl.DataAdd(_logger!, _context!, cmbSchool, tbName);
    }

    private void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            dataGridView1.ReadOnly = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;

            updateId = (int)dataGridView1.CurrentRow.Cells["ID"].Value;
            dataGridView1.CurrentRow.Cells["NAME"].Selected = true;

            _logger?.Log(LogLevel.Information, $"ID {updateId} : {dataGridView1.CurrentRow.Cells["NAME"].Value} 수정 시작");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            _logger?.Log(LogLevel.Error, ex.Message);
            throw;
        }
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            Student oldStudentName = _context!.students.Find(updateId)!;
            var newStudentName = Convert.ToString(dataGridView1.CurrentRow.Cells["NAME"].Value);

            _logger?.Log(LogLevel.Information, $"수정 완료 : {oldStudentName.name} > {newStudentName}");

            oldStudentName.name = newStudentName!;

            _context.Entry(oldStudentName).State = EntityState.Modified;
            _context.SaveChanges();

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            _logger?.Log(LogLevel.Error, ex.Message);
            throw;
        }
    }

    private void dtnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            int foundId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            Student foundStudent = _context?.students.Find(foundId)!;
            _context?.students.Remove(foundStudent);
            _context?.SaveChanges();
            //OnLoad(e);
            _logger?.Log(LogLevel.Information, $"학생 삭제 : {foundStudent.name}");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            _logger?.Log(LogLevel.Error, ex.Message);
            throw;
        }
    }
}
