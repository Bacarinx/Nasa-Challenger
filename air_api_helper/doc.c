#include <stdio.h>

double calc(double p1, double p1_v, double p2, double p2_v, double p3, double p3_v) {
    // double t_dist = p1+p2+p3;

    double p1_q = 1/p1;
    double p2_q = 1/p2;
    double p3_q = 1/p3;

    double t_q = (p1_q+p2_q+p3_q); 

    p1_q = p1_q/t_q;
    p2_q = p2_q/t_q;
    p3_q = p3_q/t_q;

    return (p1_v * p1_q) + (p2_v * p2_q) + (p3_v * p3_q);
}

int main(void) {
    // double v = 0; scanf("%f", &v);
    printf("%lf\n", calc(500, 20, 5, 2, 6, 5));

    return 0;
}
