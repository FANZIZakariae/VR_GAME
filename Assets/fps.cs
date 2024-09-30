using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;  // Utilisation de l'API XR pour les manettes et le casque VR

[RequireComponent(typeof(CharacterController))]
public class VRFPSController : MonoBehaviour
{
    public Camera playerCamera; // La caméra VR liée au casque
    public float walkSpeed = 3f; // Vitesse de marche ajustée pour VR
    public float runSpeed = 6f;  // Vitesse de course ajustée
    public float jumpPower = 7f;
    public float gravity = 10f;

    Vector3 moveDirection = Vector3.zero;
    CharacterController characterController;

    public XRNode leftHandNode = XRNode.LeftHand; // Manette gauche
    public XRNode rightHandNode = XRNode.RightHand; // Manette droite
    private InputDevice leftHandDevice;
    private InputDevice rightHandDevice;

    public bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Initialisation des manettes VR
        leftHandDevice = InputDevices.GetDeviceAtXRNode(leftHandNode);
        rightHandDevice = InputDevices.GetDeviceAtXRNode(rightHandNode);
    }

    void Update()
    {
        // Vérifier la connectivité des manettes
        if (!leftHandDevice.isValid) leftHandDevice = InputDevices.GetDeviceAtXRNode(leftHandNode);
        if (!rightHandDevice.isValid) rightHandDevice = InputDevices.GetDeviceAtXRNode(rightHandNode);

        #region Handles Movement (Déplacement avec joystick gauche)
        // Utilisation du joystick de la manette gauche pour le mouvement
        Vector2 inputAxisLeft = Vector2.zero;
        if (leftHandDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxisLeft))
        {
            Vector3 forward = playerCamera.transform.forward;  // Utilise la direction du regard pour avancer
            Vector3 right = playerCamera.transform.right;

            // Empêcher la montée/descente lors du déplacement
            forward.y = 0;
            right.y = 0;

            // Calcul de la direction de mouvement en fonction de l'input du joystick gauche
            float curSpeedX = canMove ? inputAxisLeft.y * walkSpeed : 0;
            float curSpeedY = canMove ? inputAxisLeft.x * walkSpeed : 0;
            float movementDirectionY = moveDirection.y;
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            moveDirection.y = movementDirectionY;

            // Ajout de la gravité si le joueur n'est pas au sol
            if (!characterController.isGrounded)
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }
        }
        #endregion

        #region Handles Running (Course avec le joystick droit)
        // Utilisation du joystick de la manette droite pour détecter si le joueur veut courir
        Vector2 inputAxisRight = Vector2.zero;
        if (rightHandDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxisRight))
        {
            // Si le joystick droit est poussé vers l'avant (axe Y positif), passer en mode course
            if (inputAxisRight.y > 0.5f) // 0.5f est une sensibilité pour "avant"
            {
                moveDirection *= runSpeed / walkSpeed; // Multiplier la vitesse de mouvement
            }
        }
        #endregion

        #region Handles Jumping (Saut avec bouton VR)
        bool jumpInput;
        if (rightHandDevice.TryGetFeatureValue(CommonUsages.primaryButton, out jumpInput) && jumpInput && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        #endregion

        // Appliquer le mouvement
        characterController.Move(moveDirection * Time.deltaTime);
    }
}
