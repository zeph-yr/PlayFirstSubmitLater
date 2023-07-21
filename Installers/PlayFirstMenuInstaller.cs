using Zenject;

namespace PlayFirst.Installers
{
    internal sealed class PlayFirstMenuInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ModifierUI>().AsSingle();
        }
    }
}
