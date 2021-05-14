# AudiometryMultiClassClassification

C# ML.NET program to predict one of the following possible hearing loss types:
1. Conductive
2. Mixed
3. None (No Hearing Loss)
4. Sensorineural

---

## Multi-Class Classification Metrics:

1. Micro Accuracy
2. Macro Accuracy
3. Log-Loss
4. Log-Loss Reduction

These four metrics are represented on a scale of 0.00 to 1.00. For a high-performing model, the log-loss should be close to 0.00. Macro-accuracy, micro-accuracy, and log-loss reduction should be close to 1.00.<sup>[1](https://docs.microsoft.com/en-us/dotnet/machine-learning/resources/metrics)</sup>

### Current Mutli-Class Metrics

Here are the latest mutli-class metrics for this program:
```
   Macro Accuracy     = 1
   Micro Accuracy     = 1
   Log-Loss           = 0.0019
   Log-Loss Reduction = 0.9985
```

---

## Resources
[1] Microsoft. (2019, December 17). Evaluate your ML.NET model with metrics. https://docs.microsoft.com/en-us/dotnet/machine-learning/resources/metrics
