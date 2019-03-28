using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Spire.Doc;
using System.IO;
using System.Web;

namespace Word
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Load documents
            Document docTitle = new Document();
            docTitle.LoadFromFile(@"SAUSAGE问题.docx", FileFormat.Docx);

            string str = docTitle.GetText();
            str = str.Replace("\r", "\r\r\r");
            //string strnew;
            //strnew = LanChange(str);


            string[] sArray = str.Split('\n');
            int i = 0;
            string sHeadline;
            while (sArray[i] != "")
            {
                if (IsHeadline(sArray[i].Substring(0, 3)))
                {
                    sHeadline = sArray[i];
                    sHeadline = sHeadline.Replace("\t", "");
                    sHeadline = sHeadline.Replace(" ", "");
                    Directory.CreateDirectory(sHeadline);
                    i++;

                    while ((IsTitle(sArray[i].Substring(0, 3))) && (!IsHeadline(sArray[i].Substring(0, 3))))
                    {
                        MDFile file = new MDFile();

                        file.Title = sArray[i];
                        i++;
                        while ((!IsTitle(sArray[i].Substring(0, 3))) && ((!IsHeadline(sArray[i].Substring(0, 3)))))
                        {
                            file.Content += sArray[i];
                            i++;
                        }

                        //保存MD文件
                        string sss = sHeadline + "//" + file.Title + ".md";
                        sss = sss.Replace(" ", "");
                        sss = sss.Replace("\r", "");
                        sss = sss.Replace("\t", "");
                        sss = sss.Replace("？", "");
                        sss = sss.Replace("，", ",");
                        sss = sss.Replace("、", "");
                        sss = sss.Replace("（", "(");
                        sss = sss.Replace("）", ")");
                        sss = sss.Replace("。", ".");
                        sss = sss.Replace("%", "");
                        sss = sss.Replace(":", "");
                        sss = sss.Replace("问题：", ".");
                        //FileStream fd = new FileStream(sss, FileMode.Create);

                        StreamWriter fd = new StreamWriter(sss, false, Encoding.GetEncoding("UTF-8"));

                        string str1 = LanChange( "### " + file.Title.Replace("问题：", ""));
                        //byte[] byteArray1 = System.Text.Encoding.Default.GetBytes(str1);
                        //fd.Write(byteArray1, 0, byteArray1.Length);
                        fd.Write(str1);

                        string str2 = LanChange( "---" + "\n");
                        //byte[] byteArray2 = System.Text.Encoding.Default.GetBytes(str2);
                        //fd.Write(byteArray2, 0, byteArray2.Length);
                        fd.Write(str2);

                        if (file.Content != null)
                        {
                            string aaa = file.Content;
                            aaa = aaa.Replace("解答：", "");
                            //byte[] byteArray3 = System.Text.Encoding.Default.GetBytes(aaa);
                            //fd.Write(byteArray3, 0, byteArray3.Length);
                            fd.Write(aaa);

                        }
                        //fd.Write(byteArray2, 0, byteArray2.Length);
                        fd.Write(str2);

                        fd.Close();

                    }



                }

            }


            //Save and Launch
            docTitle.SaveToFile("Merge.docx", FileFormat.Docx);
            docTitle.SaveToFile("Sample.txt", FileFormat.Txt);
        }

        public bool IsHeadline(string str)
        {
            if (str.Contains("1. ") || str.Contains("2. ") || str.Contains("3. ") || str.Contains("4. ") || str.Contains("5. ") || str.Contains("6. ") || str.Contains("7. ") || str.Contains("8. ") || str.Contains("9. ") || str.Contains("10. "))
                return true;
            else
                return false;
        }
        public bool IsTitle(string str)
        {
            if (str.Contains("1.") || str.Contains("2.") || str.Contains("3.") || str.Contains("4.") || str.Contains("5.") || str.Contains("6.") || str.Contains("7.") || str.Contains("8.") || str.Contains("9.") || str.Contains("10."))
                return true;
            else
                return false;
        }
        string LanChange(string str)
        {
            Encoding utf8;
            Encoding gb2312;
            utf8 = Encoding.GetEncoding("UTF-8");
            gb2312 = Encoding.GetEncoding("GB2312");
            byte[] gb = gb2312.GetBytes(str);
            gb = Encoding.Convert(gb2312, utf8, gb);
            return utf8.GetString(gb);
        }
        public class MDFile
        {
            public string Title { get; set; }
            public string Content { get; set; }
        }

    }
}
