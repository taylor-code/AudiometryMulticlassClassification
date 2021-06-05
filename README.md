# AudiometryMultiClassClassification

C# ML.NET program to predict the type, degree, and configuration of hearing loss.

Model Training Time: 5&ndash;6 minutes

---

## Classifications

### Type
Predicts one of the following hearing loss types:
1. Conductive
2. Mixed
3. None (No Hearing Loss)
4. Sensorineural

### Degree
The main degrees are:
1. Normal
2. Slight
3. Mild
4. Moderate
5. Moderately-Severe
6. Severe
7. Profound

The program may classify combinations of the degrees, such as:
- AC: Profound | BC: Severe
- Left: Mild & Right: Slight

### Configuration
The main configurations are:
1. Bilateral
2. Unilateral
3. Symmetrical
4. Asymmetrical
5. Low-Frequency
6. High-Frequency

The program may classify combinations of the configurations, such as:
- Bilateral | Symmetrical
- Low-Frequency | Bilateral

---

## Multi-Class Classification Metrics:

1. Micro Accuracy
2. Macro Accuracy
3. Log-Loss
4. Log-Loss Reduction

These four metrics are represented on a scale of 0.00 to 1.00. For a high-performing model, the log-loss should be close to 0.00. Macro-accuracy, micro-accuracy, and log-loss reduction should be close to 1.00.<sup>[1](https://docs.microsoft.com/en-us/dotnet/machine-learning/resources/metrics)</sup>

### Current Mutli-Class Metrics

Here are the mutli-class metrics for this program (as of 6/5/2021):
```
   Macro Accuracy     = 0.8696
   Micro Accuracy     = 0.9498
   Log-Loss           = 0.1738
   Log-Loss Reduction = 0.9227
```

---

## Resource
[1] Microsoft. (2019, December 17). Evaluate your ML.NET model with metrics. https://docs.microsoft.com/en-us/dotnet/machine-learning/resources/metrics
