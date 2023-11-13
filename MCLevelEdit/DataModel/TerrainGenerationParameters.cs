using CommunityToolkit.Mvvm.ComponentModel;

namespace MCLevelEdit.DataModel
{
    public class TerrainGenerationParameters : ObservableObject
    {
        private ushort _seed = 49098;
        private ushort _offset = 48953;
        private ushort _raise = 1140;
        private ushort _gnarl = 98;
        private ushort _river = 49;
        private ushort _lriver = 84;
        private ushort _source = 136;
        private ushort _snLin = 13;
        private byte _snFlt = 97;
        private byte _bhLin = 31;
        private ushort _bhFlt = 35;
        private ushort _rkSte = 10;

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

        public ushort LRiver
        {
            get { return _lriver; }
            set { SetProperty(ref _lriver, value); }
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

        public byte SnFlt
        {
            get { return _snFlt; }
            set { SetProperty(ref _snFlt, value); }
        }

        public byte BhLin
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
