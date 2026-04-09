# Test Technique — Développeur .NET
## Retail/E-Commerce — Équipe Fraud Detection

---

## Contexte

Tu rejoins l'équipe **Fraud Detection** de XXXXX.
Cette équipe est responsable d'analyser chaque commande passée sur le site et de décider si elle présente un risque de fraude **avant** qu'elle soit validée et expédiée.

Chaque année, plusieurs millions d'euros sont en jeu. Une fausse alerte coûte un client. Un faux négatif coûte de l'argent.

Le système actuel est un **monolithe legacy**. Ta mission est de poser les fondations d'un **microservice de scoring de fraude moderne**, que l'équipe pourra enrichir et faire évoluer.

---

## Ta mission

Développer une **API REST en .NET 10** qui expose un service de scoring de fraude sur les commandes e-commerce.

---

## Fonctionnalités attendues

### 1. Endpoint de scoring

**`POST /api/v1/orders/score`**

Calcule un score de risque pour une commande et retourne une décision.

**Payload :**
```json
{
  "orderId": "ORD-12345",
  "customerId": "CUST-789",
  "amount": 1250.00,
  "currency": "EUR",
  "items": [
    {
      "productId": "PROD-1",
      "name": "iPhone 15 Pro",
      "quantity": 2,
      "unitPrice": 625.00
    }
  ],
  "billingAddress": {
    "country": "FR",
    "city": "Bordeaux",
    "zipCode": "33000"
  },
  "shippingAddress": {
    "country": "FR",
    "city": "Paris",
    "zipCode": "75001"
  },
  "paymentMethod": "CREDIT_CARD",
  "ipAddress": "185.220.101.42",
  "customerCreatedAt": "2024-12-01T10:00:00Z",
  "orderCreatedAt": "2025-04-01T14:32:00Z"
}
```

**Réponse attendue :**
```json
{
  "orderId": "ORD-12345",
  "score": 78,
  "decision": "REVIEW",
  "triggeredRules": [
    {
      "ruleId": "HIGH_AMOUNT",
      "description": "Montant supérieur à 1000€",
      "weight": 25
    },
    {
      "ruleId": "NEW_ACCOUNT",
      "description": "Compte créé il y a moins de 30 jours",
      "weight": 30
    },
    {
      "ruleId": "BILLING_SHIPPING_MISMATCH",
      "description": "Adresses de facturation et livraison dans des villes différentes",
      "weight": 23
    }
  ],
  "processedAt": "2025-04-01T14:32:01Z"
}
```

---

### 2. Règles de détection

Tu dois implémenter **au minimum 5 règles**. Voici les règles attendues :

| RuleId | Condition | Poids |
|--------|-----------|-------|
| `HIGH_AMOUNT` | Montant > 1000 € | 25 |
| `VERY_HIGH_AMOUNT` | Montant > 5000 € | 40 |
| `NEW_ACCOUNT` | Compte créé il y a moins de 30 jours | 30 |
| `BILLING_SHIPPING_MISMATCH` | Ville de facturation ≠ ville de livraison | 23 |
| `INTERNATIONAL_SHIPPING` | Adresse de livraison hors France | 35 |
| `HIGH_QUANTITY_SINGLE_ITEM` | Quantité d'un même produit > 3 | 20 |
| `SUSPICIOUS_IP` | IP appartient à une plage Tor connue : `185.220.101.0/24`, `192.42.116.0/24` | 45 |

**Les règles sont cumulatives.** Le score final est la somme des poids des règles déclenchées.

**Score → Décision :**

| Score | Décision |
|-------|----------|
| 0 – 39 | `ACCEPT` |
| 40 – 74 | `REVIEW` |
| 75 et + | `REJECT` |

---

### 3. Consultation des scores

**`GET /api/v1/orders/{orderId}/score`**
Retourne le résultat de scoring d'une commande déjà analysée.
Retourne `404` si la commande n'a jamais été scorée.

**`GET /api/v1/scores`**
Liste les scores avec filtres optionnels :
- `?decision=REJECT` — filtrer par décision
- `?from=2025-01-01&to=2025-04-01` — filtrer par date de traitement

---

### 4. Persistance

Stocke les résultats de scoring en **MongoDB**.

> Si tu ne peux pas monter MongoDB localement, une implémentation **en mémoire** est acceptable — à condition que :
> - L'interface de repository soit correctement définie
> - La substitution soit documentée dans le README
> - Le code soit clairement préparé pour la transition vers Mongo

---

## Contraintes techniques

- **Framework** : .NET 10, C# 13
- **Architecture** : séparation claire des responsabilités (tu justifieras tes choix dans le README)
- **Tests unitaires** : obligatoires sur les règles de fraude, au moins 5 cas testés
- **README** : instructions de lancement + explications des choix d'architecture
- **Git** : historique propre avec des commits atomiques et bien nommés

---

## Points bonus (non obligatoires, mais appréciés)

- Pipeline de règles **extensible** : ajouter une règle ne doit pas modifier le code existant (pense à l'Open/Closed Principle)
- `docker-compose.yml` pour lancer l'API + MongoDB d'un seul coup
- Middleware de **logging structuré** avec un correlation ID par requête
- Gestion globale des erreurs avec **ProblemDetails** (RFC 7807)
- Test d'intégration sur l'endpoint de scoring

---

## Livrable

Un **repository Git** (GitHub ou GitLab) avec :
- Le code source
- Un `README.md` clair (lancement, architecture, choix techniques)
- L'historique Git propre

**Durée estimée : 4 à 6 heures.**
Tu n'es pas jugé sur la quantité de code, mais sur la qualité et la cohérence de tes choix.

---

## Ce que l'équipe va évaluer

| Critère | Ce qu'on regarde |
|---------|-----------------|
| Clarté du code | Nommage, responsabilités, lisibilité |
| Architecture | Séparation des couches, extensibilité des règles |
| Tests | Cas couverts, qualité des assertions |
| Async | Utilisation correcte de async/await |
| Gestion d'erreurs | Cohérence, messages utiles |
| README | Peut-on lancer le projet en moins de 5 minutes ? |

---

## Questions fréquentes

**"Je peux utiliser des librairies ?"**
Oui. MediatR, FluentValidation, AutoMapper — dis-nous pourquoi tu les as choisis (ou pourquoi tu as évité tel outil).

**"Je dois implémenter toutes les règles bonus ?"**
Non. Mieux vaut 5 règles propres et bien testées que 10 règles bâclées.

**"MongoDB est obligatoire ?"**
Non, voir section Persistance. Mais l'interface doit être prête.
