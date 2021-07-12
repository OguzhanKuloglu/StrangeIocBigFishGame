using System.Collections.Generic;
using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;
using Assets.Scripts.Extensions;
using Constans;
using DG.Tweening;
using Pathfinding;
using Pathfinding.RVO;
using Sirenix.OdinInspector;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Views
{
    public class EnemyView : View
    {
        public event UnityAction<FishMealIdentity> onFeed;
        public event UnityAction<CharacterIdentity> onCrash;
        public event UnityAction onIndicatorDelete;
        public event UnityAction onElectricTrigger;
        public event UnityAction onEnemyTutorial;

        public event UnityAction<CharacterVo> onCharacterLoaded;
        
        public AIDestinationSetter AISetter;
        public RVOController RvoController;
        private CharacterIdentity CharacterIdentity;
        private AIPath Path;
        //public List<Transform> RayMap;
        [Title("Radar Map List")]
        public List<GameObject> FeedList = new List<GameObject>();
        public List<GameObject> EnemyWarning = new List<GameObject>();
        public List<GameObject> HudList = new List<GameObject>();
        
        [Title("Escape Point")]
        public Transform EscapePoint;
        private float EscapeTime = 0f;
        
        [Title("Skill")]
        public SkillType SkillType1;

        [Title("PFX")] 
        public bool usedSkill = false;
        public ParticleSystem SpeedParticle;
        public ParticleSystem InkParticle;
        public ParticleSystem ElectricParticle;
        public bool isTutorialEnemy = false;
        public bool isTutorialShow = false;
        public GameObject TutorialObject;
        protected override void Start()
        {
            base.Start();
            CharacterIdentity = GetComponent<CharacterIdentity>();
            Path = GetComponent<AIPath>();
            onCharacterLoaded?.Invoke(CharacterIdentity.Vo);
            
        }
        public void SetScale(float ScaleFactor)
        {
             var currentScale = this.transform.localScale.x + ScaleFactor;
             currentScale = currentScale < 1 ? 1 : currentScale;
             this.transform.DOScaleX(currentScale, 0.1f);
             this.transform.DOScaleY(currentScale, 0.1f);
             CharacterIdentity.Vo.Scale = currentScale;
        }
        
        public void SetProcess(float earningProcess)
        {
            CharacterIdentity.Vo.ProccessValue += earningProcess;
        }
        public float GetProcessValue()
        {
            return CharacterIdentity.Vo.ProccessValue;
        }
        public void SetTarget(Transform Target)
        {
            AISetter.target = Target;
            RvoController.enabled = true;
        }
        public void Destroy()
        {
            onIndicatorDelete?.Invoke();
            Destroy(this.gameObject);
        }
        public CharacterType GetCharacterType()
        {
            return CharacterIdentity.Vo.ChracterType;
        }
        public CharacterIdentity GetCharacterIdentity()
        {
            return CharacterIdentity;
        }
      
                
        private bool isInkSkill = false;

        public void FeedTrigger(Collider2D other)
        {
            if (other.tag == Tags.InkSkill)
            {
                isInkSkill = true;
            }

            if (other.tag == Tags.Feed)
            {
                onFeed?.Invoke(other.GetComponent<FishMealIdentity>());
                
            }
            if(other.tag == Tags.Bad)
                onFeed?.Invoke(other.GetComponent<FishMealIdentity>());

            if (other.tag == Tags.Enemy)
            {
                if (!isInkSkill)
                {
                    onCrash?.Invoke(other.GetComponentInParent<CharacterIdentity>());
                }
                
            }
           
        }

        public void TriggerExit(Collider2D other)
        {
            isInkSkill = false;
        }

        public void SkillTrigger(Collider2D other)
        {
            if (other.tag == Tags.ElectricSkill)
            {
                EscapePoint.parent = null;
                EscapePoint.position = other.gameObject.transform.parent.position + other.gameObject.transform.parent.up * -7f;
                SetTarget(EscapePoint);
                EscapeTime = 0;
                // DOTween.Kill(this);
                DOTween.To(()=> EscapeTime, x=> EscapeTime = x, 1, 3f).OnComplete(()=>GetFeedTransform());
            }
        }

        public void AddEnemyWarning(Collider2D other)
        {
            bool danger = false;
            if (other.gameObject.tag == Tags.Player)
            {
                if (CharacterIdentity.Vo.ProccessValue >
                    other.gameObject.GetComponentInParent<PlayerManager>().GetProcess())
                {
                    SetTarget(other.gameObject.transform);
                    if (isTutorialEnemy)
                    {
                        TutorialObject.SetActive(true);
                        isTutorialEnemy = false;
                        onEnemyTutorial?.Invoke();
                    }
                    return;
                }
            }

            if (other.gameObject.tag == Tags.Enemy)
            {
                if (CharacterIdentity.Vo.ProccessValue >
                    other.gameObject.GetComponentInParent<CharacterIdentity>().Vo.ProccessValue && CharacterIdentity.Vo.ProccessValue > 6)
                {
                    SetTarget(other.gameObject.transform);
                    return;
                }
            }

            if (!EnemyWarning.Contains(other.gameObject))
            {
                EnemyWarning.Add(other.gameObject);
                int useSkill = Random.Range(0, 2);
                if(!usedSkill && useSkill == 0)
                    UseSkill();
            }
            
            Debug.DrawLine(other.gameObject.transform.parent.position,other.gameObject.transform.parent.position + other.gameObject.transform.parent.up * 7f,Color.magenta,5f);
            EscapePoint.parent = null;
            EscapePoint.position = other.gameObject.transform.parent.position + other.gameObject.transform.parent.up * -7f;
            SetTarget(EscapePoint);
            EscapeTime = 0;
            DOTween.To(()=> EscapeTime, x=> EscapeTime = x, 1, 3f).OnComplete(()=>GetFeedTransform());
        }

        

        public void RemoveEnemyWarning(Collider2D other)
        {
            EnemyWarning.Remove(other.gameObject);
            GetFeedTransform();
        }
        public void AddFeed(GameObject obj)
        {
            FeedList.Add(obj);
            if(AISetter.target == null)
                GetFeedTransform();
        }

        public void RemoveFeed(GameObject obj)
        {
            FeedList.Remove(obj);
        }

        public void GetFeedTransform()
        {
            Transform target = null;
            if (FeedList.Count > 0)
            {
                target = FeedList[FeedList.Count - 1].transform;
            }
            SetTarget(target);
        }
        
        [Button("SET HUD")]
        public void SetHud(int hudIndex)
        {
           HudList.ListAllDisable();
           HudList[hudIndex].gameObject.SetActive(true);
        }
        public void SetSkill(SkillType type)
        {
            SkillType1 = type;
        }

        private void UseSkill()
        {
            switch (SkillType1)
            {
                case SkillType.Electro:
                    UseElectric();
                    break;
                case SkillType.Speed:
                    UseSpeed();
                    break;
                case SkillType.Inc:
                    UseInk();
                    break;
            }
        }

        private float SpeedSkill = 0f;
        private float SpeedSkillWait = 0f;
        [Button("USE SPEED")]
        public void UseSpeed()
        {
            usedSkill = true;
            SpeedSkill = 0f;
            SpeedSkillWait = 0f;
            SpeedParticle.Play();
            Path.speed = Path.speed * 2;
            DOTween.To(()=> SpeedSkill, x=> SpeedSkill = x, 1, 3f).OnComplete(()=>StopSpeed());
            DOTween.To(()=> SpeedSkillWait, x=> SpeedSkillWait = x, 1, 10f).OnComplete(()=>usedSkill = false);
        }
        private float ElectricSkill = 0f;
        private float ElectricSkillWait = 0f;
        [Button("USE ELECTRIC")]
        public void UseElectric()
        {
            usedSkill = true;
            ElectricSkill = 0f;
            ElectricSkillWait = 0f;
            ElectricParticle.Play();
            ElectricParticle.GetComponentInChildren<Collider2D>().enabled = true;
            DOTween.To(()=> ElectricSkill, x=> ElectricSkill = x, 1, 3f).OnComplete(()=>StopElectric());
            DOTween.To(()=> ElectricSkillWait, x=> ElectricSkillWait = x, 1, 10f).OnComplete(()=>usedSkill = false);

        }
        private float InkSkill = 0f;
        private float InkSkillWait = 0f;
        private ParticleSystem cloneInk;
        [Button("USE INK")]
        public void UseInk()
        {
            usedSkill = true;

            cloneInk = Instantiate(InkParticle, this.transform.parent);
            cloneInk.transform.localPosition = this.transform.position;
            cloneInk.transform.GetChild(1).tag = Tags.InkSkill;
            cloneInk.time = 0;
            cloneInk.Play();
            isInkSkill = true;

            DOTween.To(()=> InkSkill, x=> InkSkill = x, 1, 3f).OnComplete(()=>StopInk());
            DOTween.To(()=> InkSkillWait, x=> InkSkillWait = x, 1, 10f).OnComplete(()=>usedSkill = false);
        }
        
        public void StopSpeed()
        {
            Path.speed = Path.speed / 2;
            SpeedParticle.Stop();
        }
        public void StopElectric()
        {
            ElectricParticle.Stop();
            ElectricParticle.GetComponentInChildren<Collider2D>().enabled = false;
        }
        public void StopInk()
        {
            if (cloneInk != null)
            {
                cloneInk.Stop();
                Destroy(cloneInk.gameObject);
            }
            
            
        }
        public void CheckStatus(GameStatus status)
        {
            if (status != GameStatus.Play)
            {
                Path.enabled = false;
            }
            else
            {
                Path.enabled = true;
            }
        }
    }
    
}
