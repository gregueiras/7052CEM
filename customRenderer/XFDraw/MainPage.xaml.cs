using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XFDraw
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        private static readonly string CLEAR_TEXT = "Clear";
        private static readonly string NOT_CLEAR_TEXT = "Already Clear";

        private Boolean isCanvasDirty;
        private Command ClearCommand;
        private ToolbarItem Trash;

        public MainPage()
        {
            InitializeComponent();

            sketchView.SketchUpdated += OnSketchUpdate;
            ClearCommand = new Command(OnClearClicked, () => { return IsCanvasDirty; });

            Trash = new ToolbarItem()
            {
                Text = NOT_CLEAR_TEXT,
                Icon = "trash.png",
            };
            Trash.Clicked += (o, s) => OnClearClicked();
            ToolbarItems.Add(Trash);

            ToolbarItems.Add(new ToolbarItem("New Color", "pencil.png", OnColorClicked));
        }

        private void OnSketchUpdate(object sender, EventArgs e)
        {
            IsCanvasDirty = true;
            Trash.Text = CLEAR_TEXT;
        }

        private void OnClearClicked()
        {
            sketchView.Clear();
            IsCanvasDirty = false;
            Trash.Text = NOT_CLEAR_TEXT;
        }

        private void OnColorClicked()
        {
            sketchView.InkColor = GetRandomColor();
        }

        Random rand = new Random();
        Color GetRandomColor()
        {
            return new Color(rand.NextDouble(), rand.NextDouble(), rand.NextDouble());
        }

        Boolean IsCanvasDirty
        {
            get { return isCanvasDirty; }
            set
            {
                isCanvasDirty = value;

                ClearCommand?.ChangeCanExecute();

            }
        }
    }
}
