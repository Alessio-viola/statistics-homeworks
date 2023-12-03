# Donsker's Invariance Principle Simulation

# Parameters
n <- 1000 # Number of random variables
m <- 1000  # Number of paths
t_values <- seq(0, 1, length.out = 1000)  # Time values

# Generate random variables and compute partial sum processes
X <- matrix(rnorm(n * m), ncol = m)
partial_sums <- apply(X - colMeans(X), 2, function(col) cumsum(col) / sqrt(n))

# Plot sample paths and compare to Brownian motion
matplot(t_values, partial_sums, type = 'l', col = rep('blue', m), lty = 1, lwd = 1,
        xlab = 'Time', ylab = 'Value')  # Specify x and y axis labels

# Add Brownian motion for comparison
brownian_motion <- cumsum(rnorm(length(t_values))) / sqrt(length(t_values))
lines(t_values, brownian_motion, col = 'red', lwd = 2)

# Set plot title
title("Donsker's Invariance Principle Simulation")

# Add legend
legend('topright', legend = c('Brownian Motion', 'Paths'), col = c('red', 'blue'), lwd = c(2, 1), lty = c(1, 1))

# Check Donsker's Invariance Principle
ks_test <- ks.test(partial_sums[, 1], 'pnorm', mean = 0, sd = 1)  # Kolmogorov-Smirnov test against normal distribution
cat("Kolmogorov-Smirnov test p-value:", ks_test$p.value, "\n")
