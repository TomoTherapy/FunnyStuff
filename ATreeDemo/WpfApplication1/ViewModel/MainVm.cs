using System.Windows.Data;
using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Diagnostics;
using System.Collections.Specialized;
using WpfApplication1.Model;
using Utils;
using System.Windows.Input;
using System.IO;
using MVVM;

namespace WpfApplication1.ViewModel
{
    // MainVm for ATreeDemo

    public partial class MainVm : ViewModelBase
    {

        #region JustForSingleTreeDemo

        // Single tree for Demo 
        public NavTreeVm SingleTree { get; set; }

        // .... and some properties for setting parameters in Demo program
        private int rootNr;
        public int RootNr
        {
            get { return rootNr; }
            set
            {
                if (!SetProperty(ref rootNr, value, "RootNr")) return;
                SingleTree.RebuildTree(RootNr, includeFiles);
            }
        }

        private bool includeFiles;
        public bool IncludeFiles
        {
            get { return includeFiles; }
            set
            {
                if (!SetProperty(ref includeFiles, value, "IncludeFiles")) return;
                SingleTree.RebuildTree(RootNr, includeFiles);
            }
        }

        RelayCommand rebuildTreeCommand;
        public ICommand RebuildTreeCommand
        {
            get { return rebuildTreeCommand ?? (rebuildTreeCommand = new RelayCommand(RebuildSingleTree)); }
        }

        public void RebuildSingleTree(object p)
        {
            SingleTree.RebuildTree(RootNr, IncludeFiles);
            if (TabbedNavTrees.SelectedNavTree != null)  TabbedNavTrees.SelectedNavTree.RebuildTree();
        } 
        #endregion

        // The tabbed trees we will use in minimal "FileExplorer"..  
        public TabbedNavTreesVm TabbedNavTrees { get; set; }

        // For now SelectedPath common to all trees
        RelayCommand selectedPathFromTreeCommand;
        public ICommand SelectedPathFromTreeCommand
        {
            get
            {
                return selectedPathFromTreeCommand ??
                       (selectedPathFromTreeCommand =
                              new RelayCommand(x => SelectedPath = (x as string)));
            }
        }

        // Selected path set by command call when item is clicked
        private string selectedPath;
        public string SelectedPath
        {
            get { return selectedPath; }
            set { SetProperty(ref selectedPath, value, "SelectedPath");}
        }

        // constructor constructs Single Tree and TabbedNavTreesVm
        public MainVm()
        {
            // Construct Single tree
            SingleTree = new ViewModel.NavTreeVm();

            //Construct tabbed trees
            TabbedNavTrees = new TabbedNavTreesVm();
            TabbedNavTrees.SelectedNavTree = TabbedNavTrees.NavTrees[0];
        }
    }
}
