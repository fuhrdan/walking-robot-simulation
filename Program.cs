﻿//*****************************************************************************
//** 874. Walking Robot Simulation  leetcode                                 **
//*****************************************************************************


typedef struct {
    int x;
    int y;
} Point;

int comparePoints(const void* a, const void* b) {
    Point* p1 = (Point*)a;
    Point* p2 = (Point*)b;
    if (p1->x == p2->x) {
        return p1->y - p2->y;
    } else {
        return p1->x - p2->x;
    }
}

int max(int a, int b) {
    return (a > b) ? a : b;
}

int robotSim(int* commands, int commandsSize, int** obstacles, int obstaclesSize, int* obstaclesColSize) {
    char direction = 'n';
    int max_distance = 0;
    int x = 0, y = 0;

    Point* obstacleSet = (Point*)malloc(obstaclesSize * sizeof(Point));
    for (int i = 0; i < obstaclesSize; i++) {
        obstacleSet[i].x = obstacles[i][0];
        obstacleSet[i].y = obstacles[i][1];
    }
    qsort(obstacleSet, obstaclesSize, sizeof(Point), comparePoints);

    for (int i = 0; i < commandsSize; i++) {
        if (commands[i] != -1 && commands[i] != -2) {
            for (int j = 0; j < commands[i]; j++) {
                int new_x = x, new_y = y;
                if (direction == 'n') {
                    new_y++;
                } else if (direction == 'e') {
                    new_x++;
                } else if (direction == 's') {
                    new_y--;
                } else if (direction == 'w') {
                    new_x--;
                }

                Point new_pos = {new_x, new_y};
                if (bsearch(&new_pos, obstacleSet, obstaclesSize, sizeof(Point), comparePoints) == NULL) {
                    x = new_x;
                    y = new_y;
                    max_distance = max(max_distance, x * x + y * y);
                } else {
                    break;
                }
            }
        } else if (commands[i] == -1) {
            if (direction == 'n') {
                direction = 'e';
            } else if (direction == 'e') {
                direction = 's';
            } else if (direction == 's') {
                direction = 'w';
            } else if (direction == 'w') {
                direction = 'n';
            }
        } else if (commands[i] == -2) {
            if (direction == 'n') {
                direction = 'w';
            } else if (direction == 'w') {
                direction = 's';
            } else if (direction == 's') {
                direction = 'e';
            } else if (direction == 'e') {
                direction = 'n';
            }
        }
    }

    free(obstacleSet);
    return max_distance;
}