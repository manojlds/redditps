using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Provider;

namespace redditps
{
    [CmdletProvider("Reddit", ProviderCapabilities.None)]
    public class RedditProvider : ContainerCmdletProvider, IContentCmdletProvider
    {
        public IContentReader GetContentReader(string path)
        {
            throw new System.NotImplementedException();
        }

        public object GetContentReaderDynamicParameters(string path)
        {
            throw new System.NotImplementedException();
        }

        public IContentWriter GetContentWriter(string path)
        {
            throw new System.NotImplementedException();
        }

        protected override void GetChildItems(string path, bool recurse)
        {
            base.GetChildItems(path, recurse);
        }

        protected override object GetChildItemsDynamicParameters(string path, bool recurse)
        {
            return base.GetChildItemsDynamicParameters(path, recurse);
        }

        protected override void GetChildNames(string path, ReturnContainers returnContainers)
        {
            base.GetChildNames(path, returnContainers);
        }

        protected override object GetChildNamesDynamicParameters(string path)
        {
            return base.GetChildNamesDynamicParameters(path);
        }

        protected override bool HasChildItems(string path)
        {
            return base.HasChildItems(path);
        }

        protected override void GetItem(string path)
        {
            base.GetItem(path);
        }

        protected override object GetItemDynamicParameters(string path)
        {
            return base.GetItemDynamicParameters(path);
        }

        protected override bool ItemExists(string path)
        {
            return base.ItemExists(path);
        }

        protected override object ItemExistsDynamicParameters(string path)
        {
            return base.ItemExistsDynamicParameters(path);
        }

        protected override Collection<PSDriveInfo> InitializeDefaultDrives()
        {
            return new Collection<PSDriveInfo>
                       {
                           new RedditDriveInfo("reddit", ProviderInfo, "reddit:", "Reddit Provider", null, true)
                       };
        }

        public object GetContentWriterDynamicParameters(string path)
        {
            throw new System.NotImplementedException();
        }

        public void ClearContent(string path)
        {
            throw new System.NotImplementedException();
        }

        public object ClearContentDynamicParameters(string path)
        {
            throw new System.NotImplementedException();
        }

        protected override bool IsValidPath(string path)
        {
            throw new System.NotImplementedException();
        }


    }
}
