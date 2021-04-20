using System;
using System.Collections.Generic;
using System.Linq;
using EventHook.Tools;

namespace JobData
{
    public class ObjectManager
    {
        public const string DEFAULT_OBJECTNAME = "obj";

        private readonly Config config;
        private readonly ActObjects actObjects;

        public ActObjects Objects { get { return actObjects; } }

        public ObjectManager()
        {
            config = new Config();
            actObjects = new ActObjects(Config.OBJECTS, config.DictDir);
        }

        public void Add(ActObject obj)
        {
            bool isNew = !obj.Id.HasValue;
            if (isNew || !Exists(obj.Id.Value))
            {
                actObjects.Add<ActObject>(obj);
                if (isNew)
                {
                    obj.Id = getMaxId() + 1;
                }
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

        private int getMaxId()
        {
            int res = 0;
            List<ActObject> objs = actObjects.GetItems<ActObject>();
            if (objs.Count > 0)
            {
                res = objs.Max(obj => obj.Id ?? 0);
            }
            return res;
        }
    }
}
