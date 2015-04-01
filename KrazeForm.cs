using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KrazeForms
{
    class KrazeForm : Form
    {
        private BoardView board;

        public KrazeForm()
        {
            this.board = new BoardView();
            this.board.Visible = true;
            this.ClientSize = this.board.Size;
            this.Visible = true;
            this.Name = "Kraze";
            this.Text = "Kraze";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.Controls.Add(this.board);
            KeyDown += this.KeyDownEvent;
            KeyPress += this.KeyPressEvent;
            KeyUp += this.KeyUpEvent;
        }

        public void KeyDownEvent(object sender, KeyEventArgs e)
        {
            this.board.KeyDownEvent(sender, e);
        }

        public void KeyPressEvent(object sencer, KeyPressEventArgs e)
        {
            this.board.Board_KeyPress(sencer, e);
        }

        public void KeyUpEvent(object sender, KeyEventArgs e)
        {
            this.board.KeyUpEvent(sender, e);
        }
    }
}
