using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PadMonsterInfo;
using System.Drawing;

namespace PadHelper___Win.UI
{
    class MonsterSelect : System.Windows.Forms.Form 
    {
        private TextBox monsterSearchBox;
        private Button searchButton;
        private List<MonsterCard> searchResults;
        private MonsterDB db;
        private ListView monsterSelectionView;
        private ImageList monsterImages;
        public MonsterCard MonsterSelection;

        public MonsterSelect(MonsterDB DB)
        {
            db = DB;
            this.Width = 500;
            this.Height = 600;
            MonsterSelection = null;

            monsterSearchBox = new TextBox();
            monsterSearchBox.Left = this.Padding.Left;
            monsterSearchBox.Width = this.ClientSize.Width;       
            this.Controls.Add(monsterSearchBox);

            searchButton = new Button();
            searchButton.Top = -20;
            searchButton.Left = 0;
            this.Controls.Add(searchButton);
            this.AcceptButton = searchButton;
            searchButton.Click += SearchButton_Click;
                        
            monsterImages = new ImageList();
            monsterImages.ImageSize = new Size(60, 60);

            monsterSelectionView = new ListView();
            monsterSelectionView.Top = monsterSearchBox.Top + monsterSearchBox.Height;
            monsterSelectionView.Width = this.ClientSize.Width;
            monsterSelectionView.Height = this.ClientSize.Height - monsterSearchBox.Height;
            monsterSelectionView.SmallImageList = monsterImages;
            monsterSelectionView.View = View.Details;
            var Column1 = monsterSelectionView.Columns.Add("Monsters", monsterSelectionView.ClientSize.Width, HorizontalAlignment.Left);
            //monsterSelectionView.Columns.Add("Name",monsterSelectionView.ClientSize.Width - 62);
            monsterSelectionView.MultiSelect = false;
            monsterSelectionView.Activation = ItemActivation.OneClick;
            monsterSelectionView.ItemActivate += MonsterSelectionView_SelectedIndexChanged;
            //monsterSelectionView.SelectedIndexChanged += MonsterSelectionView_SelectedIndexChanged;
            this.Controls.Add(monsterSelectionView);
        }

        private void MonsterSelectionView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (monsterSelectionView.SelectedIndices.Count > 0)
            {
                MonsterSelection = searchResults[monsterSelectionView.SelectedIndices[0]];
                this.DialogResult = DialogResult.OK;
                this.Close();
            }

            //throw new NotImplementedException();
        }

        private async void SearchButton_Click(object sender, EventArgs e)
        {
            searchResults = db.FindMonsters(monsterSearchBox.Text);
            monsterSelectionView.Items.Clear();
            monsterImages.Images.Clear();
            for(int i = 0; i < searchResults.Count(); i++)
            {
                MonsterCard monster = searchResults[i];
                Image img = await db.getMonsterImage60(monster);
                monsterImages.Images.Add(img);
                ListViewItem itm = new ListViewItem();
                itm.Text = monster.Name;
                itm.SubItems.Add(monster.Name);
                itm.ImageIndex = i;
                monsterSelectionView.Items.Add(itm);
            }
        }
    }
}
