using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace XMLparser
{
    public partial class Form1 : Form
    {
        private IEnumerable<KeyValuePair<string, int>> keyValuePairs;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.RestoreDirectory = true;
            openFile.DefaultExt = ".xml";
            //openFile.Filter = "Text documents (.xml)|*.xml";

            if(openFile.ShowDialog() == DialogResult.OK)
            {
                Cursor cursor = Cursors.WaitCursor;
                Dictionary<string, int> keyValuePairs = new Dictionary<string, int>();
                using (XmlTextReader reader = new XmlTextReader(openFile.OpenFile()))
                {
                    keyValuePairs.Add("no-role", 0);
                    while (reader.Read())
                    {
                        if(reader.NodeType == XmlNodeType.Element)
                        {
                            if (reader.HasAttributes)
                            {
                                string key = reader.GetAttribute(0);
                                if (!keyValuePairs.ContainsKey(key))
                                {
                                    keyValuePairs.Add(key, 1);
                                }
                                else
                                {
                                    //int value = keyValuePairs[key];
                                    keyValuePairs[key] = ++keyValuePairs[key];
                                }
                            }
                        }
                        else
                        {
                            keyValuePairs["no-role"] = ++keyValuePairs["no-role"];
                        }
                    }
                }

                foreach (KeyValuePair<string, int> entry in keyValuePairs)
                {
                    rtb.Text += entry.Key + " : " + entry.Value + "\r\n";
                }
                //Cursor.Current = Cursors.Default;
            }
        }
    }
}
