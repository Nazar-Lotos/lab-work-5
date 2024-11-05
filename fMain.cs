using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace lab_work_5
{
    public partial class fMain : Form
    {
        public fMain()
        {
            InitializeComponent();
        }

        private void fMain_Load(object sender, EventArgs e)
        {
            gvTVs.AutoGenerateColumns = false;

            DataGridViewColumn column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Brand";
            column.Name = "Бренд";
            gvTVs.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Model";
            column.Name = "Модель";
            gvTVs.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "ScreenSize";
            column.Name = "Діагональ екрану";
            gvTVs.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Resolution";
            column.Name = "Роздільна здатність";
            gvTVs.Columns.Add(column);

            column = new DataGridViewCheckBoxColumn();
            column.DataPropertyName = "IsSmartTV";
            column.Name = "Smart TV";
            gvTVs.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "SoundPower";
            column.Name = "Потужність звуку";
            gvTVs.Columns.Add(column);

            bindSrcTVs.Add(new Television("Samsung", "QLED-Q80T", 55, "4K", true, 20));
            EventArgs args = new EventArgs(); OnResize(args);
        }
        private void fMain_Resize(object sender, EventArgs e) 
        {
            int buttonSize = 9 * btnAdd.Width + 3 * tsSeparator1.Width;
            btnEdit.Margin = new Padding(Width - buttonSize, 0, 0, 0);
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            Television television = new Television();

            fTelevision ft = new fTelevision(television);
            if(ft.ShowDialog() == DialogResult.OK) 
            {
                bindSrcTVs.Add(television);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Television television = (Television)bindSrcTVs.List[bindSrcTVs.Position];

            fTelevision ft = new fTelevision(ref television);
            if(ft.ShowDialog() == DialogResult.OK)
            {
                bindSrcTVs.List[bindSrcTVs.Position] = television;
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Видалити поточний запис?", "Видалення запису", 
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                bindSrcTVs.RemoveCurrent();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Очистити таблицю?\n\nВсі дані будуть втрачені", "Очищення даних", 
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) 
            {
                bindSrcTVs.Clear();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Закрити застосунок?", "Вихід з програми", 
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK ) 
            {
                Application.Exit();
            }
        }

        private void btnSaveAsText_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "Текстові файли (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.Title = "Зберегти дані у текстовому форматі";
            saveFileDialog.InitialDirectory = Application.StartupPath;
            StreamWriter sw;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                sw = new StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8);
                try
                {
                    foreach (ElectronicDevice device in bindSrcTVs.List)
                    {
                        sw.Write(device.Brand + "\t" + device.Model + "\t" +
                                 device.ScreenSize + "\t" + device.Resolution + "\t" +
                                 device.IsSmartTV + "\t" + device.SoundPower + "\t\n");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Сталася помилка: \n{ex.Message}",
                                    "Помилка",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
                finally
                {
                    sw.Close();
                }
            }
        }

        private void btnSaveAsBinary_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "Файли даних (*.televisions)|*.televisions|All files (*.*)|*.*";
            saveFileDialog.Title = "Зберегти дані у бінарному форматі";
            saveFileDialog.InitialDirectory = Application.StartupPath;
            BinaryWriter bw;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                bw = new BinaryWriter(saveFileDialog.OpenFile());
                try
                {
                    foreach (ElectronicDevice television in bindSrcTVs.List)
                    {
                        bw.Write(television.Brand);
                        bw.Write(television.Model);
                        bw.Write(television.ScreenSize);
                        bw.Write(television.Resolution);
                        bw.Write(television.IsSmartTV);
                        bw.Write(television.SoundPower);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Сталася помилка: \n{0}", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    bw.Close();
                }
            }
        }

        private void btnOpenFromText_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Текстові файли (*.txt)|*.txt|All files (*.*) | *.* ";
            openFileDialog.Title = "Прочитати дані у текстовому форматі";
            openFileDialog.InitialDirectory = Application.StartupPath;
            StreamReader sr;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                bindSrcTVs.Clear();
                sr = new StreamReader(openFileDialog.FileName, Encoding.UTF8);
                string s;

                try
                {
                    while ((s = sr.ReadLine()) != null)
                    {
                        string[] split = s.Split('\t');
                        ElectronicDevice device = new Television(split[0], split[1], int.Parse(split[2]),
                            split[3], bool.Parse(split[4]), int.Parse(split[5]));
                        bindSrcTVs.Add(device);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Сталася помилка: \n{0}", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    sr.Close();
                }
            }

        }

        private void btnOpenFromBinary_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Файли даних (*.televisions)|*.televisions|All files (*.*) | *.* ";
            openFileDialog.Title = "Прочитати дані у бінарному форматі";
            openFileDialog.InitialDirectory = Application.StartupPath;
            BinaryReader br;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                bindSrcTVs.Clear();
                br = new BinaryReader(openFileDialog.OpenFile());
                try
                {
                    ElectronicDevice device;
                    while (br.BaseStream.Position < br.BaseStream.Length)
                    {
                        device = new Television();
                        for (int i = 1; i <= 6; i++)
                        {
                            switch (i)
                            {
                                case 1:
                                    device.Brand = br.ReadString();
                                    break;
                                case 2:
                                    device.Model = br.ReadString();
                                    break;
                                case 3:
                                    device.ScreenSize = br.ReadInt32();
                                    break;
                                case 4:
                                    device.Resolution = br.ReadString();
                                    break;
                                case 5:
                                    device.IsSmartTV = br.ReadBoolean();
                                    break;
                                case 6:
                                    device.SoundPower = br.ReadInt32();
                                    break;
                            }
                        }
                        bindSrcTVs.Add(device);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Сталася помилка: \n{0}", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    br.Close();
                }
            }

        }
    }
}
