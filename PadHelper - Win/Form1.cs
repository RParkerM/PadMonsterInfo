using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PadMonsterInfo;
using System.Diagnostics;

namespace PadHelper___Win
{
    public partial class Form1 : Form
    {
        MonsterDB db;
        List<PictureBox> materialImages;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            db = new MonsterDB();
            MonsterCard monster = db.FindMonsterCard(1746);
            textBox1.Text = monster.Image40.HRef;
            textBox2.Text = monster.Image60.HRef;
            pictureBox1.Width = 60;
            pictureBox1.Height = 60;
            this.AcceptButton = button1;
        }

        private void clearFlowLayout(FlowLayoutPanel panel)
        {
            List<Control> controls = panel.Controls.Cast<Control>().ToList();
            foreach(Control control in controls)
            {
                panel.Controls.Remove(control);
                control.Dispose();
            }
        }

        private void addCardToPanel(MonsterCard card, FlowLayoutPanel panel)
        {
            string phUrl = "https://www.padherder.com/";
            PictureBox pb = new PictureBox();
            pb.Size = new Size(64, 64);
            //Methods.UpdateFileCached(phUrl + card.Image60.HRef, card.Image60.FileName);
            //pb.BackgroundImage = Image.FromFile(card.Image60.FileName);
            setMonsterImageAsync(pb, card);
            pb.BackgroundImageLayout = ImageLayout.Center;
            //pb.BackColor = Color.Blue;
            panel.Controls.Add(pb);
        }

        async private void setMonsterImageAsync(PictureBox pictureBox, MonsterCard card)
        {
            pictureBox.BackgroundImage = await db.getMonsterImage60(card);
        }
        

        private void addEvolutions(MonsterCard card)
        {
            foreach(PadMonsterInfo.Evolutions.Evolution evo in db.getAvailableEvolutions(card))
            {
                MonsterCard evoCard = db.FindMonsterCard(evo.EvolvesTo);
                addCardToPanel(evoCard, Evolutions);
            }
        }

        private void buttonclick()
        {
            int monsterCard;
            if (int.TryParse(textBox1.Text, out monsterCard))
            {
                MonsterCard monster = db.FindMonsterCard(monsterCard);
                if (monster != null)
                {
                    string phUrl = "https://www.padherder.com/";
                    clearFlowLayout(Evolutions);
                    addEvolutions(monster);

                    Methods.UpdateFileCached(phUrl + monster.Image40.HRef, monster.Image40.FileName);
                    Methods.UpdateFileCached(phUrl + monster.Image60.HRef, monster.Image60.FileName);
                    setMonsterImageAsync(pictureBox1, monster);
                    //pictureBox1.Image = await db.getMonsterImage60(monster);//Image.FromFile(monster.Image60.FileName);
                    clearFlowLayout(LayoutMaterials);
                    textBox2.Text = LayoutMaterials.Controls.Count.ToString();
                    int materialCount = 0;
                    if (monster.Evolutions.Count > 0)
                    {
                        materialCount = monster.Evolutions.First().Materials.Count();
                        foreach (PadMonsterInfo.Evolutions.EvoMaterial material in monster.Evolutions.First().Materials)
                        {
                            PictureBox pb = new PictureBox();
                            pb.Size = new Size(60, 60);
                            MonsterCard materialCard = db.FindMonsterCard(material.MonsterCardId);
                            Methods.UpdateFileCached(phUrl + materialCard.Image60.HRef, materialCard.Image60.FileName);
                            pb.Image = Image.FromFile(materialCard.Image60.FileName);
                            LayoutMaterials.Controls.Add(pb);
                        }
                    }

                    label1.Text = monster.Name;
                }
            }
            else
            {
                textBox2.Text = "Invalid MonsterID";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            buttonclick();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var form = new UI.MonsterSelect(db);
            form.ShowDialog();
            textBox1.Text = form.MonsterSelection.Name;
            form.Dispose();
        }
    }
}
