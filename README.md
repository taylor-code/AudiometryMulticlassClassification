# Audiometry Multi-Class Classification

C# ML.NET program to predict the type, degree, and configuration of hearing loss.

---

## Performance

The employed multi-class classification metrics are:

1. Micro Accuracy
2. Macro Accuracy
3. Log-Loss
4. Log-Loss Reduction

These four metrics are represented on a scale of 0.00 to 1.00. For a high-performing model, the log-loss should be close to 0.00. Macro-accuracy, micro-accuracy, and log-loss reduction should be close to 1.00.<sup>[1](https://docs.microsoft.com/en-us/dotnet/machine-learning/resources/metrics)</sup>

### Model's Performance

The average performance metrics for this program are:
```
   Build Time         = 8:26 (Min:Sec)
   Prediction Time    = 623 ms
   Macro Accuracy     = 0.7422
   Micro Accuracy     = 0.9824
   Log-Loss           = 0.0784
   Log-Loss Reduction = 0.9574
```

Class representation accounts for the disparity between the macro-average accuracy and the micro-average accuracy. A disproportionate class representation is expected in audiology. Given the class imbalance, micro accuracy is a better representation of this model's performance.

---

## Classifications

The program predicts the following categories:

#### Type
1. Normal (No Hearing Loss)
2. Conductive
3. Sensorineural
4. Mixed

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

### Classification Example

Given the decibel values:
```json
  {
    "Left Ear": {
      "AC": {
        "250 Hz": 35,
        "500 Hz": 40,
        "1000 Hz": 35,
        "2000 Hz": 30,
        "4000 Hz": 40,
        "8000 Hz": 35
      },
      "BC": {
        "250 Hz": 20,
        "500 Hz": 20,
        "1000 Hz": 25,
        "2000 Hz": 20,
        "4000 Hz": 20,
        "8000 Hz": 20
      }
    },
    "Right Ear": {
      "BC": {
        "250 Hz": -10,
        "500 Hz": 10,
        "1000 Hz": 0,
        "2000 Hz": 5,
        "4000 Hz": 45,
        "8000 Hz": 45
      },
      "AC": {
        "250 Hz": -5,
        "500 Hz": -5,
        "1000 Hz": -10,
        "2000 Hz": -5,
        "4000 Hz": 40,
        "8000 Hz": 30
      }
    }
  }
```

The application correctly predicts the following labels:
```
Type   = "Mixed"
Degree = "Left: AC: Mild | BC: Slight & Right: AC: Moderate | BC: Mild"
Config = "Right: High-Frequency | Bilateral | Asymmetrical"
```

---

## Resource
[1] Microsoft. (2019, December 17). Evaluate your ML.NET model with metrics. https://docs.microsoft.com/en-us/dotnet/machine-learning/resources/metrics
