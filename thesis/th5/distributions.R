# Set seed for reproducibility
set.seed(123)

# Simulate data from Normal Distribution
n_normal <- 1000
mean_normal <- 0
sd_normal <- 1
data_normal <- rnorm(n_normal, mean = mean_normal, sd = sd_normal)

# Simulate data from Uniform Distribution
n_uniform <- 1000
min_uniform <- 0
max_uniform <- 1
data_uniform <- runif(n_uniform, min = min_uniform, max = max_uniform)

# Simulate data from Binomial Distribution
n_binomial <- 1000
size_binomial <- 10
prob_binomial <- 0.5
data_binomial <- rbinom(n_binomial, size = size_binomial, prob = prob_binomial)

# Simulate data from Poisson Distribution
n_poisson <- 1000
lambda_poisson <- 3
data_poisson <- rpois(n_poisson, lambda = lambda_poisson)

# Plotting
jpeg("output_plot.jpg", width = 800, height = 600)
par(mfrow = c(2, 2))

hist(data_normal, main = "Normal Distribution", col = "skyblue", xlab = "Value", probability = TRUE)
curve(dnorm(x, mean = mean_normal, sd = sd_normal), col = "blue", lwd = 2, add = TRUE)

hist(data_uniform, main = "Uniform Distribution", col = "lightgreen", xlab = "Value", probability = TRUE)
curve(dunif(x, min = min_uniform, max = max_uniform), col = "green", lwd = 2, add = TRUE)

hist(data_binomial, main = "Binomial Distribution", col = "lightcoral", xlab = "Value", probability = TRUE)
x_values <- seq(0, size_binomial, by = 1)
points(x_values, dbinom(x_values, size = size_binomial, prob = prob_binomial), col = "red", pch = 16)

hist(data_poisson, main = "Poisson Distribution", col = "lightyellow", xlab = "Value", probability = TRUE)
x_values <- seq(0, max(data_poisson), by = 1)
points(x_values, dpois(x_values, lambda = lambda_poisson), col = "orange", pch = 16)

dev.off()  # Chiudi il dispositivo grafico
