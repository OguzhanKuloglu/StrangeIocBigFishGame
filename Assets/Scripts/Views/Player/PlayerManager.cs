using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;
using Assets.Scripts.Extensions;
using Assets.Scripts.Model;
using Constans;
using DG.Tweening;
using Sirenix.OdinInspector;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using zModules.SaveSystemModule.Services;
using System.Collections;

namespace Assets.Scripts.Views
{
    public class PlayerManager : View
    {
        public event UnityAction<string> onTestEvent;
        public event UnityAction<FishMealIdentity> onFeed;
        public event UnityAction<CharacterIdentity> onCrash;
        public event UnityAction<CharacterVo> onCharacterLoaded;
        public event UnityAction<CharacterVo> onCharacterToLeaderboard;

        public Transform FishHolder;
        public Transform ParticleHolder;
        public ParticleSystem BombParticle;
        public ParticleSystem BadObjParticle;
        public ParticleSystem EvolveParticle;
        public ParticleSystem SpeedParticle;
        public ParticleSystem InkParticle;
        public ParticleSystem ElectricParticle;
        public ParticleSystem UnicornParticle;
        public float Speed = 0;
        private Rigidbody2D rb;
        public bool isStarted;
        public bool isInkSkill;
        public bool isUnicornSkill = false;
        private CharacterIdentity CharacterIdentity;

        public GameObject FeedTriger;

        
        protected override void Start()
        {
            base.Start();
            SetComponents();   
        }


        private void FixedUpdate()
        {
            if(!isStarted)
                return;
            Move();

        }


        public void RestartPlayer()
        {
            this.transform.position = Vector3.zero;
            this.transform.localScale = Vector3.one;
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
        
        public void Move()
        {
            rb.velocity = transform.up * (Time.fixedDeltaTime * Speed);
        }
        
        
        public void SetRotate(float joystick,float rotateSensivity)
        {
            if(!isStarted)
                return;
           transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.Euler(0f,0f,joystick), rotateSensivity);
        }
        public void SetIsStarted(bool value = false)
        {
            isStarted = value;
        }

        public void SetSpeed(float speed)
        {
            Speed = speed;
        }
        public void SetComponents()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        public void SetScale(float ScaleFactor)
        {
             var currentScale = this.transform.localScale.x + ScaleFactor;
             this.transform.DOScaleX(currentScale, 0.1f);
             this.transform.DOScaleY(currentScale, 0.1f);
        }
        public void ResetScale()
        {
            this.transform.DOScaleX(1f, 0f);
            this.transform.DOScaleY(1f, 0f);
        }
        public void LoadPlayer(GameObject playerFish)
        {
            FishHolder.DestroyChildren();
            var go = Instantiate(playerFish, FishHolder);
            go.transform.parent = FishHolder;
            go.transform.localPosition = Vector3.zero;
            CharacterIdentity = go.gameObject.GetComponent<CharacterIdentity>();
            onCharacterLoaded?.Invoke(CharacterIdentity.Vo);
            onCharacterToLeaderboard?.Invoke(CharacterIdentity.Vo);
        }

        public void Evolve(GameObject playerFish)
        {
            PlayEvolveParticle();
            LoadPlayer(playerFish);
        }

        public void OnExitTrigger(Collider2D other)
        {
            if (other.tag == Tags.InkSkill)
            {
                isInkSkill = false;
            }
        }

        public void FeedTrigger(Collider2D other)
        {
            if (other.tag == Tags.InkSkill)
                isInkSkill = true;

            if (other.tag == Tags.Feed)
                onFeed?.Invoke(other.GetComponent<FishMealIdentity>());
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

        public void PlayBadOjbParticle()
        {
            var inst = Instantiate(BadObjParticle, this.transform.parent);
            inst.transform.localPosition = FeedTriger.transform.position;
            inst.time = 0;
            inst.Play();
        }

        public void PlayBombParticle()
        {
            var inst = Instantiate(BombParticle, this.transform.parent);
            inst.transform.localPosition = FeedTriger.transform.position;
            inst.time = 0;
            inst.Play();
        }


        public void PlayFeedParticle()
        {
            var inst = Instantiate(EvolveParticle, this.transform.parent);
            inst.transform.localPosition = FeedTriger.transform.position;
            inst.time = 0;
            inst.Play();
        }

        public void PlayEvolveParticle()
        {
            var inst = Instantiate(EvolveParticle, this.transform.parent);
            inst.transform.localPosition = FeedTriger.transform.position;
            inst.time = 0;
            inst.Play();
        }

        public void PlayUnicornParticle()
        {
            UnicornParticle.Play();
        }

        public void PlaySpeedParticle(bool value)
        {
            if (value)
            {
                SpeedParticle.time = 0;
                SpeedParticle.Play();
            }
            else
            {
                SpeedParticle.Stop();
            }
        }
        public void PlayElectricParticle(bool value)
        {
            if (value)
            {
                ElectricParticle.time = 0;
                ElectricParticle.Play();
                ElectricParticle.GetComponentInChildren<Collider2D>().enabled = true;
            }
            else
            {
                ElectricParticle.Stop();
                ElectricParticle.GetComponentInChildren<Collider2D>().enabled = false;
            }
            
        }
        private ParticleSystem clone;
        public void PlayInkParticle(bool value)
        {
            if (value)
            {

                clone = Instantiate(InkParticle, this.transform.parent);
                clone.transform.localPosition = FishHolder.transform.position;
                clone.transform.GetChild(1).tag = Tags.InkSkill;
                clone.time = 0;
                clone.Play();
                isInkSkill = true;
            }
            else
            {
                if (clone != null)
                {
                    clone.Stop();
                    isInkSkill = false;
                    Destroy(clone.gameObject);
                    Debug.Log(" is ink skill statu : " + isInkSkill);
                }
            }

        }
        public void SetProcess(float value)
        {
            if (value < 0)
                return;
            CharacterIdentity.Vo.ProccessValue = value;
        }
        public float GetProcess()
        {
            return CharacterIdentity.Vo.ProccessValue;
        }
        public CharacterIdentity GetCharacterIdentity()
        {
            return CharacterIdentity;
        }
        public void SetSimulate(bool value = true)
        {
            rb.simulated = value;
        }
        public void SetIsUnicornSkill(bool value = true)
        {
            isUnicornSkill = value;
        }
        public bool GetIsUnicornSkill()
        {
            return isUnicornSkill;
        }
        public void CheckStatus(GameStatus status)
        {
            if (status != GameStatus.Play)
            {
                isStarted = false;
                SetSimulate(false);
            }
            else
            {
                isStarted = true;
                SetSimulate(true);
            }
        }


        public void ReturnGameAfterRewardedAd()
        {
            FeedTriger.SetActive(false);
            StartCoroutine(corActiveTrigger());
        }

        IEnumerator corActiveTrigger()
        {
            yield return new WaitForSeconds(3f);
            FeedTriger.SetActive(true);
            yield return null;
        }
    }
    
}
