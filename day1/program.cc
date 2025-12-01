#include <iostream>
#include <fstream>
#include <algorithm>
#include <vector>

using namespace std;

int main(int argc, char* argv[])
{
    cout << "--\nAoC 2025 Day 1\n--\n";
    // For testing
    int debuglimit = 1;
    int debug = 0;
    bool debugapply = false;

    // User args
    string filename = "input";
    if (argc == 1) {
        cout << "Assume default input file '" << filename << "'\n";
    }
    else if (argc > 1) {
        filename = argv[1];
        cout << "Taking CLI input file name '" << filename << "'\n";
    }
    if (argc > 2) {
        cout << "Reading 2nd input param\n    -d / --debug for debug printing\n";
        if (string{argv[2]} == "-d" || string{argv[2]} == "--debug") {
            debugapply = true;
        }
        else {
            debugapply = false;
        }
    }
    if (debugapply) {
        cout << "--\nDEBUG MODE : ON\nLINE LIMIT : " << debuglimit << "\n--" << endl;
    }

    // Input file
    ifstream input(filename);

    // Variables for output
    long sum = 0;

    // Read file
    string line;
    int num = 50; // Start dial on 50
    while (getline(input, line) && (!debugapply || debug < debuglimit))
    {
        debug++;
        if (debugapply) {
            cout << line << endl;
        }
        // L or R
        bool right = (line[0] == 'R');
        int value = stoi(line.substr(1));
        if (right)
        {
            num = (num + value) % 100;
        }
        else
        {
            num = (num + 100 - value) % 100;
        }
        if (num == 0)
        {
            sum++;
        }
    }

    // Finished with input file
    input.close();

    // Output
    cout << "--\n";
    cout << "Sum = " << sum << endl;

    cout << "--\nEnd.\n";
    return 0;
}

