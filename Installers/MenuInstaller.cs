using Zenject;

namespace PlayFirst.Installers
{
    internal class PlayFirstMenuInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ModifierUI>().AsSingle();
        }
    }
}
