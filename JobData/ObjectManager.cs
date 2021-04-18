using System;
using EventHook.Tools;

namespace JobData
{
    public class ObjectManager
    {
        public const string DEFAULT_OBJECTNAME = "obj";

        private Config config;
        private ActObjects actObjects;

        public ActObjects Objects { get { return actObjects; } }

        public ObjectManager()
        {
            config = new Config();
            actObjects = new ActObjects(Config.OBJECTS, config.DictDir);
        }

        public void Add(ActObject obj)
        {
            if (!Exists(obj.Id.Value))
            {
                actObjects.Add<ActObject>(obj);
            }
        }

        public ActObject Get(int id) 
        {
            return actObjects.GetItem<ActObject>(id);
        }

        public bool Exists(int id)
        {
            ActObject existedObj = actObjects.GetItem<ActObject>(id);
            return existedObj != null;
        }

        public string getUniqueObjName()
        {
            return getUniqueName("obj");
        }

        public string getUniqueName(string prefix)
        {
            return $"{prefix}_{NumberUtils.GetUniqueNumber()}";
        }
    }
}
