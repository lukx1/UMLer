﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UMLer.Controls;
using UMLer.Tools;

namespace UMLer
{
    public class Diagram
    {
        private ITool _SelectedTool = new Pointer();
        public ITool SelectedTool
        {
            get => _SelectedTool;
            set
            {
                var existingTool = ToolBox.Where(r => r.GetType() == value.GetType()).SingleOrDefault();
                if (existingTool == null)
                {
                    ToolBox.Add(value);
                    _SelectedTool = value;
                    
                }
                else
                {
                   _SelectedTool = existingTool;
                }
                
                RaiseSelectedToolChanged(new EventArgs());
            }
        }
        public string StatusText { get => StatusLabel.Text;
            set
            {
                StatusLabel.Text = value;
                RaiseStatusTextChanged(new EventArgs());
            }
        }
        private Label StatusLabel;
        private PropertyGrid propertyGrid;
        private ElementPanel elementPanel;

        public static int GenerateID()
        {
            return IDCounter++;
        }

        private static int IDCounter = 0;

        public ElementPanel ElementPanel => elementPanel;

        private HashSet<ITool> ToolBox = new HashSet<ITool>();

        public void AddToToolBox(ITool tool)
        {
            ToolBox.Add(tool);
        }

        public event EventHandler SelectedToolChanged;
        public event EventHandler StatusTextChanged;

        public static bool Deserializing = false;
        public static readonly double DistFromLinkClickAccept = 12.0;
        public static float[] FocusDashPattern => new float[] { 4f, 2f };
        public static readonly int FocusDistanceFromBox = 10;
        public static readonly float FocusPenWidthMultiplier = 4f;
        public static readonly float PenDefaultWidth = 2f;

        public Diagram()
        {
            Bind();
        }

        protected virtual void Bind()
        {
            this.SelectedToolChanged += Diagram_SelectedToolChanged;
            this.SelectedToolChanged += (object o, EventArgs e) => ChangePropertyGridVal(SelectedTool);
        }

        private void Diagram_SelectedToolChanged(object sender, EventArgs e)
        {
            StatusText = "Selected " + SelectedTool.Name;
        }

        protected virtual void RaiseSelectedToolChanged(EventArgs a)
        {
            SelectedToolChanged?.Invoke(this, a);
        }

        protected virtual void RaiseStatusTextChanged(EventArgs a)
        {
            StatusTextChanged?.Invoke(this, a);
        }

        private void ChangePropertyGridVal(object o)
        {
            if (propertyGrid == null)
            {
                throw new NullReferenceException("Property grid is not bound to an object");
            }
            if(o == null)
            {
                throw new NullReferenceException("Can't bind to a null object");
            }

            propertyGrid.SelectedObject = o;
        }

        public void BindElementPanel(ElementPanel elementPanel)
        {
            this.elementPanel = elementPanel;
            this.elementPanel.FocusChanged += (object o, PaintableEventArgs a) => ChangePropertyGridVal(a.IPaintable);
        }

        public void BindProperty(PropertyGrid propertyGrid)
        {
            this.propertyGrid = propertyGrid;
        }

        public void BindStatusLabel(Label StatusLabel)
        {
            this.StatusLabel = StatusLabel;
        }

    }
}
