using Avalonia.Data.Converters;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Globalization;

namespace MCLevelEdit.DataModel
{
    public class TerrainGenerationParameters : ObservableObject
    {
        private ushort _seed;
        private ushort _offset;
        private ushort _raise;
        private ushort _gnarl;
        private ushort _river;
        private ushort _source;
        private ushort _snLin;
        private ushort _snFlt;
        private ushort _bhLin;
        private ushort _bhFlt;
        private ushort _rkSte;

        public ushort Seed
        {
            get { return _seed; }
            set { SetProperty(ref _seed, value); }
        }

        public ushort Offset
        {
            get { return _offset; }
            set { SetProperty(ref _offset, value); }
        }

        public ushort Raise
        {
            get { return _raise; }
            set { SetProperty(ref _raise, value); }
        }

        public ushort Gnarl
        {
            get { return _gnarl; }
            set { SetProperty(ref _gnarl, value); }
        }

        public ushort River
        {
            get { return _river; }
            set { SetProperty(ref _river, value); }
        }

        public ushort Source
        {
            get { return _source; }
            set { SetProperty(ref _source, value); }
        }

        public ushort SnLin
        {
            get { return _snLin; }
            set { SetProperty(ref _snLin, value); }
        }

        public ushort SnFlt
        {
            get { return _snFlt; }
            set { SetProperty(ref _snFlt, value); }
        }

        public ushort BhLin
        {
            get { return _bhLin; }
            set { SetProperty(ref _bhLin, value); }
        }

        public ushort BhFlt
        {
            get { return _bhFlt; }
            set { SetProperty(ref _bhFlt, value); }
        }

        public ushort RkSte
        {
            get { return _rkSte; }
            set { SetProperty(ref _rkSte, value); }
        }
    }
}
