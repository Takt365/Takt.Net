/**
 * Page de connexion · Français
 * - Structure : fields, login, forgot, sign (identique à zh-CN)
 */
export default {
  /** Champs : label, placeholder, validation — communs aux trois pages */
  fields: {
    username: {
      label: "Nom d'utilisateur",
      placeholder: "Entrez le nom d'utilisateur",
      validation: {
        required: "Entrez le nom d'utilisateur",
        min: '3 caractères minimum',
        max: '20 caractères maximum'
      }
    },
    password: {
      label: 'Mot de passe',
      placeholder: 'Entrez le mot de passe',
      validation: {
        required: 'Entrez le mot de passe',
        min: '6 caractères minimum',
        max: '50 caractères maximum'
      }
    },
    confirmPassword: {
      label: 'Confirmer le mot de passe',
      placeholder: 'Entrez à nouveau le mot de passe',
      validation: {
        required: 'Entrez à nouveau le mot de passe',
        mismatch: 'Les mots de passe ne correspondent pas'
      }
    },
    userEmail: {
      label: 'Adresse e-mail',
      placeholder: 'Entrez l\'adresse e-mail',
      placeholderForgot: 'Entrez votre adresse e-mail',
      validation: {
        required: 'Entrez l\'adresse e-mail',
        format: 'Adresse e-mail valide requise'
      }
    },
    realName: {
      label: 'Nom complet',
      placeholder: 'Entrez le nom complet',
      validation: {
        required: 'Entrez le nom complet',
        max: '50 caractères maximum'
      }
    },
    userPhone: {
      label: 'Numéro de téléphone',
      placeholder: 'Entrez le numéro de téléphone',
      validation: {
        required: 'Entrez le numéro de téléphone',
        format: 'Numéro de téléphone valide requis'
      }
    },
    captcha: {
      validation: {
        required: 'Veuillez compléter la vérification',
        typeRequired: 'Le type de captcha n\'a pas été renvoyé par le serveur, vérifiez la configuration backend'
      }
    }
  },

  /** Page de connexion principale */
  login: {
    title: 'Bienvenue',
    rememberMe: 'Se souvenir de moi',
    login: 'Se connecter',
    logout: 'Se déconnecter',
    noAccountRegister: 'Nouveau sur Takt365 ? {register}'
  },

  /** Mot de passe oublié */
  forgot: {
    title: 'Mot de passe oublié',
    subtitle: 'Entrez votre e-mail pour recevoir un lien de réinitialisation',
    submit: 'Réinitialiser le mot de passe',
    backToLogin: 'Retour à la connexion',
    success: 'E-mail envoyé. Vérifiez votre boîte de réception',
    fail: "Échec de l'envoi. Vérifiez votre adresse e-mail",
    emailNotRegistered: "Cette adresse e-mail n'est pas enregistrée. Veuillez confirmer",
    protectedUser: "La récupération du mot de passe n'est pas autorisée pour ce compte"
  },
  
  /** Inscription (title = titre·bouton·{register} dans noAccountRegister) */
  sign: {
    title: "S'inscrire",
    subtitle: 'Créez votre compte pour commencer',
    hasAccount: 'Déjà un compte ? Se connecter',
    success: 'Inscription réussie. Connectez-vous',
    successInitialPassword: 'Inscription réussie. Consultez votre e-mail pour le mot de passe initial.',
    fail: "Échec de l'inscription"
  }
}
