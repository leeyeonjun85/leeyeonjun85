using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyUtiles
{
    public class Uteils
    {
        public Uteils() { }

        public static string OpenFile()
        {
            OpenFileDialog openFileDialog = new()
            {
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    //DialogResult okCancle = MessageBox.Show(
                    //    text: $"이 파일을 시뮬레이션 하시겠습니까?" +
                    //        $"{openFileDialog.FileName}",
                    //    caption: "파일 선택", MessageBoxButtons.OKCancel,
                    //    icon: MessageBoxIcon.Question);
                    //if (okCancle == DialogResult.OK)
                    //{
                    //    return openFileDialog.FileName;
                    //}
                    return openFileDialog.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        text: $"에러가 발생하였습니다." +
                            $"{Environment.NewLine}Error message: {ex.Message}",
                        caption: "Error", MessageBoxButtons.OK,
                        icon: MessageBoxIcon.Error);
                }
            }
            
            return "";
        }
    }
}
