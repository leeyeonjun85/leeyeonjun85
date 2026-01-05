using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Windows.Forms;


namespace EFCore_Oracle.Service
{
    public class getOracleDataTable
    {
        #region 필요한 필드 선언
        OracleConnection? con;
        OracleDependency? dep;
        OracleCommand? cmd;
        OracleDataAdapter adapter;
        DataTable dataTable;
        DataGridView oracleDataGridView;
        TextBox oracleTextBox;
        #endregion

        // 오라클 정보를 입력합니다.
        string constr = "User Id=testuser1;Password=0330;Data Source=localhost:1521/XEPDB1;";
        string oracleTable = "STUDENT";

        public getOracleDataTable(DataGridView dataGridView1, TextBox textBox1)
        {
            var addLine = Environment.NewLine;

            // 오라클 데이터가 보여질 테이블객체를 지정합니다.
            oracleDataGridView = dataGridView1;
            // 메시지가 기록될 텍스트박스객체를 지정합니다.
            oracleTextBox = textBox1;

            #region Oracle Settings
            // Set SQL
            string selectSql = $"SELECT * FROM {oracleTable}";

            // Create the connection
            con = new OracleConnection(constr);

            // Create Command
            cmd = new OracleCommand(selectSql, con);
            con.Open();

            // Set the port number for the listener to listen for the notification request
            // 오라클 메뉴얼에는 포트설정하라고 하지만, 안해도 됨
            //OracleDependency.Port = 1005;

            // Create the adapter
            adapter = new OracleDataAdapter(selectSql, con);

            // Create an OracleDependency instance
            dep = new OracleDependency(cmd);

            // Create the DataTable
            dataTable = new DataTable();

            // Fill the DataTable with initial data
            adapter.Fill(dataTable);

            // Bind the DataTable to the DataGridView
            oracleDataGridView.DataSource = dataTable;

            // Notification registration remove setting
            // https://docs.oracle.com/en/database/oracle/oracle-database/21/odpnt/NotificationRequestIsNotifiedOnce.html#GUID-03A223C3-36DA-482D-8962-178D7CC7C43F
            cmd.Notification.IsNotifiedOnce = false;

            // Settings registration
            cmd.ExecuteNonQuery();
            con.Close();

            // Change Event Add
            dep.OnChange += new OnChangeEventHandler(Dependency_OnChange);
            #endregion
        }

        private void Dependency_OnChange(object sender, OracleNotificationEventArgs args)
        {
            oracleTextBox.BeginInvoke(() => oracleTextBox.Text += Environment.NewLine + "Notification 이벤트가 발생하였습니다.");

            if (args.Source == OracleNotificationSource.Data)
                switch (args.Info)
                {
                    case OracleNotificationInfo.Insert:
                        oracleTextBox.BeginInvoke(() => oracleTextBox.Text += Environment.NewLine + "삽입 입니다.");
                        dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        oracleDataGridView.BeginInvoke(() => oracleDataGridView.DataSource = dataTable);
                        break;
                    case OracleNotificationInfo.Delete:
                        oracleTextBox.BeginInvoke(() => oracleTextBox.Text += Environment.NewLine + "삭제 입니다.");
                        dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        oracleDataGridView.BeginInvoke(() => oracleDataGridView.DataSource = dataTable);
                        break;
                    case OracleNotificationInfo.Update:
                        oracleTextBox.BeginInvoke(() => oracleTextBox.Text += Environment.NewLine + "수정 입니다.");
                        dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        oracleDataGridView.BeginInvoke(() => oracleDataGridView.DataSource = dataTable);
                        break;
                }
        }
    }
}
