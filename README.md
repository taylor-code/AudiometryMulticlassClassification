# AudiometryMultiClassClassification

C# ML.NET program to predict the type, degree, and configuration of hearing loss.

Model Training Time: 5&ndash;6 minutes

---

## Multi-Class Classification Metrics:

1. Micro Accuracy
2. Macro Accuracy
3. Log-Loss
4. Log-Loss Reduction

These four metrics are represented on a scale of 0.00 to 1.00. For a high-performing model, the log-loss should be close to 0.00. Macro-accuracy, micro-accuracy, and log-loss reduction should be close to 1.00.<sup>[1](https://docs.microsoft.com/en-us/dotnet/machine-learning/resources/metrics)</sup>

### Current Mutli-Class Metrics

The mutli-class metrics for this program are:
```
   Macro Accuracy     = 0.8696
   Micro Accuracy     = 0.9498
   Log-Loss           = 0.1738
   Log-Loss Reduction = 0.9227
```

---

## Classifications

The program predicts the following categories:

#### Type
1. Conductive
2. Mixed
3. None (No Hearing Loss)
4. Sensorineural

#### Degree
1. Normal
2. Slight
3. Mild
4. Moderate
5. Moderately-Severe
6. Severe
7. Profound

#### Configuration
1. Bilateral
2. Unilateral
3. Symmetrical
4. Asymmetrical
5. Low-Frequency
6. High-Frequency

### Classification Examples

#### Example 1

Given the decibel values:
```json
  {
    "AC": {
      "Left Ear": {
        "250 Hz": 40,
        "500 Hz": 45,
        "1000 Hz": 55,
        "2000 Hz": 45,
        "4000 Hz": 45,
        "8000 Hz": 45
      },
      "Right Ear": {
        "250 Hz": 50,
        "500 Hz": 50,
        "1000 Hz": 40,
        "2000 Hz": 45,
        "4000 Hz": 45,
        "8000 Hz": 45
      }
    },
    "BC": {
      "Left Ear": {
        "250 Hz": 50,
        "500 Hz": 45,
        "1000 Hz": 45,
        "2000 Hz": 55,
        "4000 Hz": 45,
        "8000 Hz": 55
      },
      "Right Ear": {
        "250 Hz": 50,
        "500 Hz": 50,
        "1000 Hz": 50,
        "2000 Hz": 45,
        "4000 Hz": 45,
        "8000 Hz": 50
      }
    }
  }
```

The application correctly predicts the following labels:
```
Type   = "Sensorineural"
Degree = "Moderate"
Config = "Bilateral | Symmetrical"
```

#### Example 2

Given the decibel values:
```json
  {
    "AC": {
      "Left Ear": {
        "250 Hz": 40,
        "500 Hz": 30,
        "1000 Hz": 20,
        "2000 Hz": 20,
        "4000 Hz": 15,
        "8000 Hz": 5
      },
      "Right Ear": {
        "250 Hz": 30,
        "500 Hz": 30,
        "1000 Hz": 25,
        "2000 Hz": 15,
        "4000 Hz": 15,
        "8000 Hz": 10
      }
    },
    "BC": {
      "Left Ear": {
        "250 Hz": 35,
        "500 Hz": 30,
        "1000 Hz": 25,
        "2000 Hz": 15,
        "4000 Hz": 10,
        "8000 Hz": 15
      },
      "Right Ear": {
        "250 Hz": 40,
        "500 Hz": 30,
        "1000 Hz": 25,
        "2000 Hz": 15,
        "4000 Hz": 10,
        "8000 Hz": 10
      }
    }
  }
```

The application correctly predicts the following labels:
```
Type   = "Left: Sensorineural & Right: Mixed"
Degree = "Left: Mild & Right: AC: Slight | BC: Mild"
Config = "Low-Frequency | Bilateral"
```

---

## Resource
[1] Microsoft. (2019, December 17). Evaluate your ML.NET model with metrics. https://docs.microsoft.com/en-us/dotnet/machine-learning/resources/metrics
