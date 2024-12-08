#include <iostream>
#include <cstdlib>
#include <climits>
#include <tuple>
#include "omp.h"

using namespace std;

const int SIZE = 50000000;

long long myArray[SIZE];

void fill_my_array_by_rand();
void fill_my_array_by_order();
long long get_sum_array(int);
long long get_sum_array_doubling(int);
tuple<int, int> get_min_element(int);

int main() {
    //fill_my_array_by_rand();
    fill_my_array_by_order();

    omp_set_nested(1);

    double timeBefore = omp_get_wtime();
#pragma omp parallel sections
    {
#pragma omp section
        {
            cout << "1 thread sum = " << get_sum_array(1) << endl;
            cout << "2 threads sum = " << get_sum_array(2) << endl;
            cout << "3 threads sum = " << get_sum_array(3) << endl;
            cout << "4 threads sum = " << get_sum_array(4) << endl;
        }
#pragma omp section
        {
            tuple<int, int> result1 = get_min_element(1);
            cout << "1 thread min element = " << get<0>(result1) << "; index = " << get<1>(result1) << endl;
            tuple<int, int> result2 = get_min_element(2);
            cout << "2 threads min element = " << get<0>(result2) << "; index = " << get<1>(result2) << endl;
            tuple<int, int> result3 = get_min_element(3);
            cout << "3 threads min element = " << get<0>(result3) << "; index = " << get<1>(result3) << endl;
            tuple<int, int> result4 = get_min_element(4);
            cout << "4 threads min element = " << get<0>(result4) << "; index = " << get<1>(result4) << endl;
        }
    }

    cout << "4 threads sum (doubling method) = " << get_sum_array_doubling(4) << endl;

    double timeAfter = omp_get_wtime();

    cout << "Total time - " << timeAfter - timeBefore << " seconds" << endl;
    return 0;
}

void fill_my_array_by_rand() {
    for (long long i = 0; i < SIZE; i++) {
        myArray[i] = rand() % 1000;
    }
}

void fill_my_array_by_order() {
    for (long long i = 0; i < SIZE; i++) {
        myArray[i] = i;
    }
}

long long get_sum_array(int threadsCount) {
    long long sum = 0;
    double timeBefore = omp_get_wtime();
#pragma omp parallel for reduction(+:sum) num_threads(threadsCount)
    for (int i = 0; i < SIZE; i++) {
        sum += myArray[i];
    }
    double timeAfter = omp_get_wtime();

    cout << "Sum of " << threadsCount << " threads worked - " << timeAfter - timeBefore << " seconds" << endl;

    return sum;
}

long long get_sum_array_doubling(int threadsCount) {
    int currentSize = SIZE;
    double timeBefore = omp_get_wtime();
    while (currentSize > 1) {
        int half = currentSize / 2;

#pragma omp parallel num_threads(threadsCount)
        {
#pragma omp for
            for (int i = 0; i < half; i++) {
                myArray[i] += myArray[currentSize - i - 1];
            }
        }

        currentSize = currentSize % 2 == 1 ? half + 1 : half;
    }
    double timeAfter = omp_get_wtime();

    cout << "Sum (doubling method) of " << threadsCount << " threads worked - " << timeAfter - timeBefore << " seconds" << endl;

    return myArray[0];
}

tuple<int, int> get_min_element(int threadsCount) {
    int minElementValue = INT_MAX;
    int minElementIndex = -1;
    double timeBefore = omp_get_wtime();
#pragma omp parallel num_threads(threadsCount)
    {
        int localMinElementValue = INT_MAX;
        int localMinElementIndex = -1;

#pragma omp for
        for (int i = 0; i < SIZE; i++) {
            if (myArray[i] < localMinElementValue) {
                localMinElementValue = myArray[i];
                localMinElementIndex = i;
            }
        }

#pragma omp critical
        {
            if (localMinElementValue < minElementValue) {
                minElementValue = localMinElementValue;
                minElementIndex = localMinElementIndex;
            }
        }
    }
    double timeAfter = omp_get_wtime();

    cout << "Min of " << threadsCount << " threads worked - " << timeAfter - timeBefore << " seconds" << endl;

    return make_tuple(minElementValue, minElementIndex);
}