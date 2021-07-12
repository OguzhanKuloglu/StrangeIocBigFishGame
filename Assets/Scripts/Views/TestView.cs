using System;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Views
{
    public class TestView : View
    {
        public event UnityAction<string> onTestEvent;

        protected override void Start()
        {
            base.Start();
            Debug.Log("*** START ***");
            onTestEvent?.Invoke("View Start");
        }

        private void Update()
        {
              //Debug.DrawLine(transform.position, transform.position + -transform.up);
              RaycastHit2D hit = Physics2D.Raycast(transform.position,transform.up * 5f, Mathf.Infinity);
              Debug.DrawRay(transform.position, transform.up * 5f, Color.magenta);

              if (hit.collider != null)
              {
                  Debug.DrawLine(transform.position, hit.point, Color.yellow);
                  Debug.Log("hit: " + hit.collider.name);
              }
        }
    }
    
}
