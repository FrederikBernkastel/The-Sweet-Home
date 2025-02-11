#if !ENABLE_ERRORS

using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace RAY_Core
{
    public abstract class BaseCharacter : BaseCoreObject
    {
        //public abstract ReactiveProperty<string> NameCharacter { get; private protected set; }
        //public BaseView CurrentView { get; private protected set; } = default;

        //public abstract void BindingView(BaseView view);
    }
    //public class MainCharacterTest : BaseCharacter
    //{
    //    public override ReactiveProperty<string> NameCharacter { get; private protected set; } = new("ош");
    //    public ReactiveProperty<float> Sadness { get; private protected set; } = new(0);
    //    public ReactiveProperty<float> Hunger { get; private protected set; } = new(0);
    //    public ReactiveProperty<float> Pollution { get; private protected set; } = new(0);
    //    private protected ReactiveProperty<bool> isDead { get; set; } = new(false);
    //    public Subject<Unit> DeadEvent { get; } = new();

    //    private protected override BaseMethod<BaseCoreObject> InitEvent => InitMethod.Instance;
    //    private protected override BaseMethod<BaseCoreObject> DisposeEvent => DisposeMethod.Instance;

    //    private protected CompositeDisposable disposables { get; } = new();

    //    private class InitMethod : BaseMethod<BaseCoreObject, InitMethod>
    //    {
    //        public override void Execute(BaseCoreObject _object)
    //        {
    //            var obj = _object as MainCharacter;

    //            obj.disposables.Clear();

    //            obj.isDead
    //                .Where(u => u)
    //                .Subscribe(_ => obj._DeadEvent())
    //                .AddTo(obj.disposables);
    //            obj.Sadness
    //                .Where(u => u >= 1f)
    //                .Subscribe(_ => obj.isDead.Value = true)
    //                .AddTo(obj.disposables);
    //            obj.Hunger
    //                .Where(u => u >= 1f)
    //                .Subscribe(_ => obj.isDead.Value = true)
    //                .AddTo(obj.disposables);
    //            obj.Pollution
    //                .Where(u => u >= 1f)
    //                .Subscribe(_ => obj.isDead.Value = true)
    //                .AddTo(obj.disposables);

    //            obj.BindingView(default);

    //            baseMethod?.Execute(_object);
    //        }
    //    }
    //    private class DisposeMethod : BaseMethod<BaseCoreObject, DisposeMethod>
    //    {
    //        public override void Execute(BaseCoreObject _object)
    //        {
    //            var obj = _object as MainCharacter;

    //            baseMethod?.Execute(_object);

    //            obj.BindingView(default);

    //            obj.disposables.Dispose();
    //            obj.disposables.Clear();

    //            obj.DeadEvent.Dispose();
    //        }
    //    }

    //    public override void BindingView(BaseView view)
    //    {
    //        CurrentView?.Show(false);
    //        CurrentView?.EnableIO(false);

    //        CurrentView = view;

    //        CurrentView?.Show(true);
    //        CurrentView?.EnableIO(true);
    //    }
    //    private void _DeadEvent()
    //    {
    //        DeadEvent.OnNext(Unit.Default);
    //    }
    //}
    //public abstract class BaseFriendlyCharacter : BaseCharacter
    //{
    //    public string Name { get; private protected set; } = default;
    //    public int Level { get; private protected set; } = default;
    //    public List<BaseItem> ListItems { get; private protected set; } = default;
    //    public Dictionary<TypeStatArmor, int> PairStats { get; private protected set; } = default;
    //    public Dictionary<TypeItem, BaseItem> PairPutOnItems { get; private protected set; } = default;

    //    public BaseViewName ViewName { get; private protected set; } = default;
    //    public BaseViewLevel ViewLevel { get; private protected set; } = default;
    //    public BaseViewStats ViewStats { get; private protected set; } = default;
    //    //public ViewArmorPut ViewArmor { get; private set; }
    //    public BaseViewInventory ViewInventory { get; private protected set; } = default;
    //    public BaseViewCharacterInventory ViewCharacterInventory { get; private protected set; } = default;

    //    public abstract CharacterSO GetCharacterSO();

    //    public abstract void SetViewName(BaseViewName view);
    //    public abstract void SetViewLevel(BaseViewLevel view);
    //    public abstract void SetViewStats(BaseViewStats view);
    //    //public abstract void SetViewArmor(ViewArmorPut view);
    //    public abstract void SetViewInventory(BaseViewInventory view);
    //    public abstract void SetViewCharacterInventory(BaseViewCharacterInventory character);

    //    public abstract void SetName(string name);
    //    public abstract void SetLevel(int level);
    //    public abstract void SetStat(TypeStatArmor statType, int statValue);
    //    public abstract void AddItemToInventory(BaseItem baseItem);
    //    public abstract void SubItemFromInventory(BaseItem baseItem);
    //    public abstract void PutOnArmor(BaseItem armorItem);
    //    public abstract void PutOffArmor(TypeItem typeItem);
    //}
}
#endif