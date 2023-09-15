﻿using Avalonia.Data.Converters;
using MCLevelEdit.DataModel;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Input;

namespace MCLevelEdit.ViewModels
{
    public class TypeIdConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var typeId = (TypeId)value;
            return new KeyValuePair<int, string>(key: (int)typeId, value: Enum.GetName(typeof(TypeId), typeId));
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return (TypeId)((KeyValuePair<int, string>)value).Key;
        }
    }

    public class EntitiesViewModel : ViewModelBase
    {
        public ObservableCollection<Entity> Entities { get; }
        public ICommand AddNewEntityCommand { get; }
        public ICommand DeleteEntityCommand { get; }

        public static KeyValuePair<int, string>[] TypeIds { get; } =
            Enum.GetValues(typeof(TypeId))
            .Cast<int>()
            .Select(x => new KeyValuePair<int, string>(key: x, value: Enum.GetName(typeof(TypeId), x)))
            .ToArray();

        public EntitiesViewModel()
        {
            Entities = new ObservableCollection<Entity>();
            AddEntity(EntityTypes.I.Spawns[(int)Spawn.Flyer1]);
            AddEntity(EntityTypes.I.Creatures[(int)Creature.Archer]);

            AddNewEntityCommand = ReactiveCommand.Create(() =>
            {
                AddEntity(new EntityType(TypeId.None, 0, ""));
            });

            DeleteEntityCommand = ReactiveCommand.Create(() =>
            {
                Entities.RemoveAt(0);
            });
        }

        private void AddEntity(EntityType entityType)
        {
            Entities.Add(new Entity(Entities.Count + 1, entityType, new Position(0, 0)));
        }
    }
}
