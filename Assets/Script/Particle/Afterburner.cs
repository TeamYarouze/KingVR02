using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Afterburner : MonoBehaviour {

        // this script controls the jet's exhaust particle system, controlling the
        // size and colour based on the jet's current throttle value.
        public Color minColour; // The base colour for the effect to start at

        private ParticleSystem m_System; // The particle system that is being controlled
        private float m_OriginalStartSize; // The original starting size of the particle system
        private float m_OriginalLifetime; // The original lifetime of the particle system
        private Color m_OriginalStartColor; // The original starting colout of the particle system

        // Use this for initialization
        private void Start()
        {
            // get the aeroplane from the object hierarchy

            // get the particle system ( it will be on the object as we have a require component set up
            m_System = GetComponent<ParticleSystem>();

            // set the original properties from the particle system
            m_OriginalLifetime = m_System.main.startLifetime.constant;
            m_OriginalStartSize = m_System.main.startSize.constant;
            m_OriginalStartColor = m_System.main.startColor.color;
        }


        // Update is called once per frame
        private void Update()
        {
			ParticleSystem.MainModule mainModule = m_System.main;
			// update the particle system based on the jets throttle
			mainModule.startLifetime = Mathf.Lerp(0.0f, m_OriginalLifetime, 1.0f);
			mainModule.startSize = Mathf.Lerp(m_OriginalStartSize*.3f, m_OriginalStartSize, 1.0f);
			mainModule.startColor = Color.Lerp(minColour, m_OriginalStartColor, 1.0f);
        }

}
